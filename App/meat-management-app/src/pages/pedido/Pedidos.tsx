import { useEffect, useState } from "react";
import { Grid } from "../../components";
import { createActionColumn } from "../../functions";
import { Buyer, Order } from "../../types";
import { OrderService } from "../../api/services";
import { useNavigate } from "react-router-dom";

export function Pedidos() {
  const navigate = useNavigate();
  const [orders, setOrders] = useState<Order[]>([]);

  const columns = [
    createActionColumn({
      onEdit: (row: Order) => navigate(`/pedidos/${row.id}`),
      onDelete: async (row: Order) => {
        if (window.confirm("Deseja realmente excluir este pedido?")) {
          await OrderService.deleteOrder(row.id!);
          setOrders((prev) => prev.filter((o) => o.id !== row.id));
        }
      },
    }),
    { field: "id", headerName: "ID", width: 150 },
    {
      field: "buyer",
      headerName: "Comprador",
      width: 400,
      valueGetter: (buyer: Buyer) => buyer.name ?? ""
    },
    {
      field: "totalBRL",
      headerName: "Valor Total (R$)",
      width: 200,
      valueFormatter: (totalBrl: number) => {
        return totalBrl?.toLocaleString("pt-BR", {
          style: "currency",
          currency: "BRL",
        });
      },
    },
  ];

  useEffect(() => {
    OrderService.getAllOrders().then(setOrders);
  }, []);

  return (
    <div>
      <h1 className="page-title">Pedidos</h1>
      <Grid columns={columns} rows={orders} />
    </div>
  );
}
