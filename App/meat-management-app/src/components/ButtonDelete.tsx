import { Button, ButtonProps } from "@mui/material";
import { FaTrash } from "react-icons/fa";


type ButtonDeleteProps = ButtonProps & {
    onClick?: () => void, 
    text?: string 
};

export function ButtonDelete({ onClick, text, ...rest }: ButtonDeleteProps) {
    return (
        <Button {...rest} onClick={onClick} variant="contained" color="error">
            <div style={{display: 'flex', gap: 4, alignItems: 'center'}}>
                <FaTrash /> {text ?? "Excluir"}
            </div>    
        </Button>
    )
}