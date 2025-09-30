import "./Pedido.css";
import { useEffect, useState } from "react";
import { TextField, MenuItem, Box, Grid, Fab } from "@mui/material";
import { Buyer, Meat, Order, OrderItem } from "../../types";
import { ButtonDelete, ConfirmModal, ButtonSave } from "../../components";
import { BuyerService, OrderService, MeatService } from "../../api/services";
import { useNavigate, useParams } from "react-router-dom";
import { Add, Delete } from "@mui/icons-material";
import { getCurrencies, getCurrencySymbol } from "../../functions";
import { NumericFormat } from "react-number-format";

export function Pedido() {
    const navigate = useNavigate();
    const { id } = useParams<{ id: string }>();
    const [order, setOrder] = useState<Order>({
        total: 0,
        orderDate: new Date(),
        buyer: { name: "", document: "", address: { stateId: "", cityId: "" } },
        items: [{ price: 0, total: 0, meatId: "", currencyCode: 'BRL' } as OrderItem],
    });

    const [meats, setMeats] = useState<Meat[]>([]);
    const [buyers, setBuyers] = useState<Buyer[]>([]);
    const [errors, setErrors] = useState<any>({});
    const [confirmOpen, setConfirmOpen] = useState(false);
    const [alertOpen, setAlertOpen] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");
    const [alertTitle, setAlertTitle] = useState("Erro");

    const currencies = getCurrencies();

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setOrder((prev) => ({ ...prev, [name]: value }));
    };

    const convertDate = (date?: Date) => {
        return date ? new Date(date).toISOString().split("T")[0] : "";
    }

    useEffect(() => {
        BuyerService.getAllBuyers()
            .then(setBuyers)
            .catch((error) => {
                setAlertMessage(error?.message);
                setAlertOpen(true);
            });

        MeatService.getAllMeats()
            .then(setMeats)
            .catch((error) => {
                setAlertMessage(error?.message);
                setAlertOpen(true);
            });

        if (id) {
            OrderService.getOrderById(id)
                .then(setOrder)
                .catch((error) => {
                    setAlertMessage(error?.message);
                    setAlertOpen(true);
                });
        } else {
            setOrder((rest) => ({ ...rest, orderDate: new Date(), items: [{ price: 0, total: 0, meatId: "", currencyCode: 'BRL'}] }));
        }
    }, [id]);

    const handleChangeBuyer = (e: any) => {
        const selectedBuyer = buyers.find((b) => b.id === e.target.value);
        if (selectedBuyer) {
            setOrder((prev) => ({ ...prev, buyer: selectedBuyer }));
        }
    };

    const openAlertModal = (title: string, message: string) => {
        setAlertMessage(message);
        setAlertTitle(title);
        setAlertOpen(true);
    }

    const handleChangeItem = (index: number, field: keyof OrderItem, value: any) => {
        const newItems = [...order.items];
        newItems[index] = { ...newItems[index], [field]: value };
        setOrder((prev) => ({ ...prev, items: newItems }));
    };

    const addItem = () => {
        setOrder((prev) => ({
            ...prev,
            items: [...prev.items, { currencyCode: "BRL", quantityKg: 0, price: 0, total: 0, priceBRL: 0, meatId: "" }],
        }));
    };

    const removeItem = (index: number) => {
        const newItems = order.items.filter((_, i) => i !== index);
        setOrder((prev) => ({ ...prev, items: newItems }));
    };

    const validate = () => {
        const newErrors: any = {};
        if (!order.buyer?.id) newErrors.buyer = "Comprador obrigatório";
        if (order.items.length === 0) newErrors.items = "Adicione pelo menos um item";
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e: any) => {
        e.preventDefault();
        if (!validate()) return;

        try {
            if (id) {
                await OrderService.updateOrder(id, order);
                openAlertModal("Sucesso", "Pedido atualizado com sucesso");
            } else {
                await OrderService.createOrder(order);
                navigate("/pedidos");
            }
        } catch (error: any) {
            setAlertMessage(error?.message);
            setAlertOpen(true);
        }
    };

    const handleDelete = async () => {
        if (id) {
            try {
                await OrderService.deleteOrder(id);
                navigate("/pedidos");
            } catch (error: any) {
                setAlertMessage(error?.message);
                setAlertOpen(true);
            } finally {
                setConfirmOpen(false);
            }
        }
    };

    return (
        <div>
            <h1 className="page-title">{id ? "Editar Pedido" : "Novo Pedido"}</h1>

            <Box
                component="form"
                onSubmit={handleSubmit}
                sx={{ display: "flex", flexDirection: "column", gap: 2, width: 900, marginTop: 6, borderRadius: 2 }}
            >
                <TextField
                    select
                    name="buyer"
                    style={{ width: 400, textAlign: 'left' }}
                    label="Comprador"
                    value={order.buyer?.id || ""}
                    onChange={handleChangeBuyer}
                    error={!!errors.buyer}
                    helperText={errors.buyer}
                    fullWidth
                >
                    {buyers.map((b) => (
                        <MenuItem key={b.id} value={b.id}>
                            {b.name}
                        </MenuItem>
                    ))}
                </TextField>
                <TextField
                    style={{ width: 400, textAlign: 'left' }}
                    label="Data"
                    name="orderDate"
                    type="date"
                    value={convertDate(order.orderDate)}
                    onChange={(e) =>
                        setOrder((prev) => ({
                            ...prev,
                            orderDate: new Date(e.target.value)
                        }))
                    }
                    InputLabelProps={{ shrink: true }}
                    fullWidth
                />

                <Box>
                    <Grid container style={{ display: 'flex', margin: 'auto', alignItems: 'center', gap: 8 }}>
                        <h2>Itens do pedido</h2>
                        <Fab className="fab" size="small" color="primary" onClick={addItem}>
                            <Add />
                        </Fab>
                    </Grid>

                    {errors.items && <span style={{ color: "red" }}>{errors.items}</span>}
                    <Box
                        sx={{
                            maxHeight: 300,
                            overflowY: 'auto',
                            marginTop: 1,
                            paddingRight: 1
                        }}
                    >
                        {order.items.map((item, index) => (
                            <Grid container spacing={2} key={index} alignItems="center" marginBottom={1} marginTop={2}>

                                <Grid width={300}>
                                    <TextField
                                        InputProps={{
                                            sx: {
                                                textAlign: 'left'
                                            }
                                        }}
                                        select
                                        label="Produto"
                                        name="meatId"
                                        value={item.meatId}
                                        onChange={(e) => handleChangeItem(index, "meatId", e.target.value)}
                                        fullWidth
                                    >
                                        {meats.map((p) => (
                                            <MenuItem key={p.id} value={p.id}>{p.name}</MenuItem>
                                        ))}
                                    </TextField>
                                </Grid>

                                <Grid width={200}>
                                    <TextField
                                        InputProps={{
                                            sx: {
                                                textAlign: 'left'
                                            }
                                        }}
                                        select
                                        label="Moeda"
                                        name="currencyCode"
                                        value={item.currencyCode}
                                        onChange={(e) => handleChangeItem(index, "currencyCode", e.target.value)}
                                        fullWidth
                                    >
                                        {currencies.map((p) => (
                                            <MenuItem key={p.code} value={p.code}>{p.name}</MenuItem>
                                        ))}
                                    </TextField>
                                </Grid>
                                <Grid width={200}>
                                    <NumericFormat
                                        customInput={TextField}
                                        label="Preço"
                                        value={item.price}
                                        onValueChange={(values) => handleChangeItem(index, "price", values.floatValue || 0)}
                                        thousandSeparator="."
                                        decimalSeparator=","
                                        decimalScale={2}
                                        fixedDecimalScale
                                        allowNegative={false}
                                        prefix={getCurrencySymbol(order.items[index].currencyCode) + " "}
                                        fullWidth
                                    />
                                </Grid>
                                <Grid>
                                    <Fab size="small" color="error" onClick={() => removeItem(index)}>
                                        <Delete />
                                    </Fab>
                                </Grid>
                            </Grid>
                        ))}
                    </Box>
                </Box>

                <Grid style={{ display: "flex", justifyContent: "space-between" }}>
                    <ButtonSave text={id ? "Salvar" : "Criar Pedido"} />
                    {id && <ButtonDelete onClick={() => setConfirmOpen(true)} />}
                </Grid>
            </Box>

            <ConfirmModal
                open={confirmOpen}
                title="Confirmação"
                message="Deseja realmente excluir este pedido?"
                onConfirm={handleDelete}
                onClose={() => setConfirmOpen(false)}
            />

            <ConfirmModal
                open={alertOpen}
                title={alertTitle}
                message={alertMessage}
                hideCancel={true}
                buttonText="Ok"
                onConfirm={() => setAlertOpen(false)}
                onClose={() => setAlertOpen(false)}
            />
        </div>
    );
}
