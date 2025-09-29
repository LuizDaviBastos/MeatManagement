import { useEffect, useState } from "react";
import { ConfirmModal, Grid, createActionColumn } from "../../components";
import { MeatService } from "../../api/services";
import { Meat } from "../../types";
import { useNavigate } from "react-router-dom";
import { formatOrigin } from "../../functions";

export function Carnes() {
    const navigate = useNavigate();
    const [meats, setMeats] = useState<Meat[]>([]);
    const [confirmOpen, setConfirmOpen] = useState(false);
    const [alertOpen, setAlertOpen] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");
    const [currentMeat, setCurrentMeat] = useState<Meat>();

    const columns = [
        createActionColumn({
            onEdit: (row: Meat) => navigate(`/carnes/${row.id}`),
            onDelete: (row: Meat) => { setCurrentMeat(row); setConfirmOpen(true) },
        }),
        { field: 'id', headerName: 'ID', width: 150 },
        { field: 'name', headerName: 'Nome', width: 400 },
        { field: 'origin', headerName: 'Origem', width: 150, valueFormatter: (origin: number) => formatOrigin(origin) }
    ];

    const handleDelete = async () => {
        if(currentMeat) {
            try {
                await MeatService.deleteMeat(currentMeat.id!);
                setMeats([...meats.filter(m => m.id !== currentMeat.id)]);
            } catch (error: any) {
                setAlertMessage(error?.message);
                setAlertOpen(true);
            } finally {
                setConfirmOpen(false);
            }
        }
    }

    useEffect(() => {
        MeatService.getAllMeats()
            .then(res => setMeats(res))
            .catch(err => {
                setAlertMessage(err.message);
                setAlertOpen(true);
            });
    }, []);

    return (
        <div>
            <h1 className="page-title">Carnes</h1>
            <Grid columns={columns} rows={meats} />

            <ConfirmModal 
                open={confirmOpen}
                title="Confirmação"
                message={`Deseja realmente excluir a carne?`}
                onConfirm={handleDelete}
                onClose={() => setConfirmOpen(false)} />

            <ConfirmModal 
                open={alertOpen}
                title="Erro"
                message={alertMessage}
                hideCancel={true}
                onConfirm={() => setAlertOpen(false)}
                onClose={() => setAlertOpen(false)} />
        </div>
    )
}
