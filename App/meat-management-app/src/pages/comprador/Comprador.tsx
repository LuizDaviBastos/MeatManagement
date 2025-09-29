import { useEffect, useState } from "react";
import { TextField, MenuItem, Box, Grid } from "@mui/material";
import { Buyer, City, State } from "../../types";
import { ButtonSave } from "../../components/ButtonSave";
import { ButtonDelete, ConfirmModal } from "../../components";
import { BuyerService, LocationService } from "../../api/services";
import { useNavigate, useParams } from "react-router-dom";
import { DocumentInput } from "../../components/DocumentInput";

export function Comprador() {
    const navigate = useNavigate();
    const { id } = useParams<{ id: string }>(); 
    const [buyer, setFormData] = useState<Buyer>({
        name: "",
        document: "",
        address: { stateId: "", cityId: "" },
    });
    const [errors, setErrors] = useState<any>({});
    const [states, setStates] = useState<State[]>([]);
    const [cities, setCities] = useState<City[]>([]);
    const [confirmOpen, setConfirmOpen] = useState(false);
    const [alertOpen, setAlertOpen] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");
    const [alertTitle, setAlertTitle] = useState("Erro");

    const openAlertModal = (title: string, message: string) => {
        setAlertMessage(message);
        setAlertTitle(title);
        setAlertOpen(true);
    }

    useEffect(() => {
        const loadData = async () => {
            try {
                const statesRes = await LocationService.getAllStates();
                setStates(statesRes);
                if (id) {
                    const buyerRes = await BuyerService.getBuyerById(id);
                    if (buyerRes.address?.stateId) {
                        const citiesRes = await LocationService.getCitiesByState(buyerRes.address.stateId);
                        setCities(citiesRes);
                    }
                    setFormData(buyerRes);
                }
            } catch (error: any) {
                openAlertModal("Erro", error?.message);
            }
        };
    
        loadData();
    }, [id]);
    
    const onChangeState = async (stateId: string) => {
        setFormData(prev => ({
            ...prev,
            address: { ...prev.address, stateId, cityId: undefined }
        }));

        try {
            const citiesRes = await LocationService.getCitiesByState(stateId);
            setCities(citiesRes);
        } catch (error: any) {
            openAlertModal("Erro", error?.message);
        }
    };

    const handleDelete = async () => {
        if(id) {
            try {
                await BuyerService.deleteBuyer(id);
                openAlertModal("Sucesso", "Comprador deletado com sucesso");
                navigate('/compradores');
            } catch (error: any) {
                openAlertModal("Erro", error?.message);
            }
        }
    }

    const handleChange = (e: any) => {
        const { name, value } = e.target;

        if (name === "stateId" || name === "cityId") {
            setFormData((prev) => ({
                ...prev,
                address: { ...prev.address, [name]: value },
            }));
        } else {
            setFormData((prev) => ({ ...prev, [name]: value }));
        }
    };

    const validate = () => {
        const newErrors: any = {};
        if (!buyer.name) newErrors.name = "Nome obrigatório";
        if (!buyer.document) newErrors.document = "Documento obrigatório";
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };
    
    const handleSubmit = async (e: any) => {
        e.preventDefault();
        if (!validate()) return;

        try {
            if (id) {
                await BuyerService.updateBuyer(id, buyer);
                setAlertOpen(true);
                openAlertModal("Sucesso", "Comprador atualizado com sucesso");
            } else {
                await BuyerService.createBuyer(buyer);
                navigate('/compradores');
            }
        }
        catch(error: any) {
            openAlertModal("Erro", error?.message);
        }

        setErrors({});
    };

    return (
        <div>
            <h1  className="page-title">{id ? "Editar Comprador" : "Novo Comprador"}</h1>
            <Box
            component="form"
            onSubmit={handleSubmit}
            sx={{
                display: "flex",
                flexDirection: "column",
                gap: 2,
                width: 800,
                marginTop: 6,
                borderRadius: 2,
            }}
        >
           <TextField
                name="name"
                label="Nome"
                value={buyer.name}
                onChange={handleChange}
                error={!!errors.name}
                helperText={errors.name}
                variant="outlined"
            />

            <DocumentInput 
                style={{width: 500}} 
                name="document" 
                value={buyer.document} 
                error={!!errors.document}
                helperText={errors.document}
                onChange={handleChange} />

            <Grid container spacing={2} width={600}>
                <Grid width={200}>
                    <TextField
                        select
                        name="stateId"
                        label="Estado"
                        value={buyer.address?.stateId ?? ""}
                        onChange={(e) => onChangeState(e.target.value)} 
                        fullWidth
                    >
                        {states.map((state) => (
                            <MenuItem key={state.id} value={state.id}>
                                {state.name}
                            </MenuItem>
                        ))}
                    </TextField>
                </Grid>

                <Grid width={200}>
                    <TextField
                        select
                        name="cityId"
                        label="Cidade"
                        value={buyer.address?.cityId ?? ""}
                        onChange={handleChange}
                        fullWidth
                        disabled={!buyer.address?.stateId}
                    >
                        {buyer.address?.stateId &&
                            cities.map((city) => (
                                <MenuItem key={city.id} value={city.id}>
                                    {city.name}
                                </MenuItem>
                            ))}
                    </TextField>
                </Grid>
            </Grid>

            <Grid style={{display: 'flex', justifyContent: 'space-between'}}>
                <ButtonSave />
                { id && <ButtonDelete onClick={() => setConfirmOpen(true)} /> }
            </Grid>
        </Box>

        <ConfirmModal 
                open={confirmOpen}
                title="Confirmação"
                message={`Deseja realmente excluir o comprador?`}
                onConfirm={handleDelete}
                onClose={() => setConfirmOpen(false)} />
        <ConfirmModal 
                open={alertOpen}
                title={alertTitle}
                message={alertMessage}
                hideCancel={true}
                buttonText={"Ok"}
                onConfirm={() => setAlertOpen(false)}
                onClose={() => setAlertOpen(false)} />
        </div>
    );
}
