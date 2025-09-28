import { useEffect, useState } from "react";
import { Grid } from "./Grid";
import { BuyerService } from "../api/services";
import { Buyer } from "../types";
import { createActionColumn } from ".";

export function Compradores() {
    const [buyers, setBuyers] = useState<Buyer[]>([]);

    const columns = [
        createActionColumn({
            onEdit: (row: Buyer) => console.log("Editar", row),
            onDelete: (row: Buyer) => console.log("Excluir", row),
          }),
        { field: 'id', headerName: 'ID', width: 150 },
        { field: 'name', headerName: 'Nome', width: 400 },
        { field: 'document', headerName: 'Documento', width: 200 }
    ];

    useEffect(() => {
        BuyerService.getAllBuyers()
            .then((res) => setBuyers(res))
    }, []);

    return (
        <div>
            <h1 className="page-title">Compradores</h1>
            <Grid columns={columns} rows={buyers} />
        </div>
    )
}