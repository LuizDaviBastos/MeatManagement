import { FaEdit, FaTrash } from "react-icons/fa";

export function createActionColumn({
  onEdit,
  onDelete,
  width = 120,
}) {
  return {
    field: 'actions',
    headerName: 'Ações',
    width,
    sortable: false,
    filterable: false,
    renderCell: (params) => (
      <div style={{ display: 'flex', gap: 10 }}>
        {onEdit && (
          <FaEdit
            style={{ color: 'blue', cursor: 'pointer', width: 20, height: 20, padding: 8 }}
            onClick={() => onEdit(params.row)}
          />
        )}
        {onDelete && (
          <FaTrash
            style={{ color: 'red', cursor: 'pointer', width: 20, height: 20, padding: 8 }}
            onClick={() => onDelete(params.row)}
          />
        )}
      </div>
    ),
  };
}
