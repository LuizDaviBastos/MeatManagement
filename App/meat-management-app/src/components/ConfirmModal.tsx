import "./ConfirmModal.css"
import { Modal, Box, Typography, Button } from "@mui/material";

type ConfirmModalProps = {
    open: boolean;
    title: string;
    message: string;
    onConfirm: () => void;
    onClose: () => void;
    hideCancel?: boolean;
    confirmMessage?: string;
    buttonText?: string;
  };
  
  export function ConfirmModal({ open, title, message, onConfirm, onClose, hideCancel, buttonText = "Confirmar" }: ConfirmModalProps) {
    return (
      <Modal open={open} onClose={onClose}>
        <Box sx={{ 
              position: "absolute" as const,
              top: "50%",
              left: "50%",
              transform: "translate(-50%, -50%)",
              width: 400,
              bgcolor: "background.paper",
              borderRadius: 2,
              boxShadow: 24,
              p: 4
              }}>
          <Typography variant="h6">{title}</Typography>
          <pre>{message}</pre>
          <Box sx={{ mt: 3, display: "flex", justifyContent: "flex-end", gap: 1 }}>
             {!hideCancel &&
             <Button variant="outlined" onClick={onClose}>Cancelar</Button>
             }
            <Button variant="contained" onClick={onConfirm}>{buttonText}</Button>
          </Box>
        </Box>
      </Modal>
    );
  }
  