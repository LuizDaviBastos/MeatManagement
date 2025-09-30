import { useEffect, useState } from "react";
import { ConfirmModal, Grid } from "../../components";
import { BuyerService } from "../../api/services";
import { Buyer } from "../../types";
import { formatCpfCnpj, createActionColumn } from "../../functions"
import { useNavigate } from "react-router-dom";

export function Compradores() {
    const navigate = useNavigate();
    const [buyers, setBuyers] = useState<Buyer[]>([]);
    const [confirmOpen, setConfirmOpen] = useState(false);
    const [alertOpen, setAlertOpen] = useState(false);
    const [alertMessage, setAlertMessage] = useState("");
    const [currentBuyer, setBuyer] = useState<Buyer>();
    
    const columns = [
        createActionColumn({
            onEdit: (row: Buyer) => navigate(`/compradores/${row.id}`),
            onDelete: (row: Buyer) => { setBuyer(row); setConfirmOpen(true) },
          }),
        { field: 'id', headerName: 'ID', width: 150 },
        { field: 'name', headerName: 'Nome', width: 400 },
        { field: 'document', headerName: 'Documento', width: 200, valueFormatter: (params: string) => formatCpfCnpj(params)  }
    ];

    const handleDelete = async () => {
        if(currentBuyer) {
            try {
                await BuyerService.deleteBuyer(currentBuyer.id!);
                setBuyers([...buyers.filter(b => b.id !== currentBuyer.id)]);
            } catch (error: any) {
                setAlertMessage(error?.message);
                setAlertOpen(true);
            } finally {
                setConfirmOpen(false);
            }
        }
    }

    useEffect(() => {
        BuyerService.getAllBuyers()
            .then((res) => setBuyers(res))
    }, []);

    return (
        <div>
            <h1 className="page-title">Compradores</h1>
            <Grid columns={columns} rows={buyers} />

            <ConfirmModal 
                open={confirmOpen}
                title="Confirmação"
                message={`Deseja realmente excluir o comprador?`}
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