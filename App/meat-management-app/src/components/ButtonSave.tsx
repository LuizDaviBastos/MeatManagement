import { Button } from "@mui/material";
import { FaSave } from "react-icons/fa";

type ButtonSaveProps = { 
    onClick?: () => void,
    text?: string
}

export function ButtonSave({ onClick, text }: ButtonSaveProps) {
    return (
        <Button onClick={onClick} type="submit" variant="contained" color="primary">
            <div style={{display: 'flex', gap: 4, alignItems: 'center'}}>
                <FaSave /> {text ?? "Salvar"}
            </div>    
        </Button>
    )
}