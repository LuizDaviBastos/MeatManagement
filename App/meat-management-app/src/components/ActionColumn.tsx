import { IconButton } from "@mui/material";
import { FaEdit, FaTrash } from "react-icons/fa";
import DeleteIcon from '@mui/icons-material/Delete';
import Edit from '@mui/icons-material/Edit';

type CreateActionColumnProps = {
  onEdit: (value: any) => void,
  onDelete: (value: any) => void,
  width?: number,

}

export function createActionColumn({
  onEdit,
  onDelete,
  width = 120,
}: CreateActionColumnProps) {
  return {
    field: 'actions',
    headerName: 'Ações',
    width,
    sortable: false,
    filterable: false,
    renderCell: (params: any) => (
      <div style={{ display: 'flex', gap: 10 }}>
        {onEdit && (
          <IconButton onClick={() => onEdit(params.row)}  aria-label="delete">
            <Edit />
          </IconButton>
         
        )}
        {onDelete && (
          <IconButton color="error" onClick={() => onDelete(params.row)}  aria-label="delete">
            <DeleteIcon />
          </IconButton>
        )}
      </div>
    ),
  };
}
