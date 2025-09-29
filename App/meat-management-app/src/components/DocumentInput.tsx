import { useState, useEffect } from "react";
import { TextField, TextFieldProps } from "@mui/material";

type DocumentInputProps = TextFieldProps & {
    onChange?: (value: {target: {name: string, value: string}}) => void;
    value?: string;
};

export function DocumentInput({ value: propValue, onChange, ...rest }: DocumentInputProps) {
    const [document, setDocument] = useState(propValue || "");

    const formatDocument = (value: string) => {
        let v = value.replace(/\D/g, "");
        if (v.length > 14) v = v.slice(0, 14)
        if (v.length <= 11) {
            v = v
                .replace(/(\d{3})(\d)/, "$1.$2")
                .replace(/(\d{3})(\d)/, "$1.$2")
                .replace(/(\d{3})(\d{1,2})$/, "$1-$2");
        } else {
            v = v
                .replace(/^(\d{2})(\d)/, "$1.$2")
                .replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3")
                .replace(/\.(\d{3})(\d)/, ".$1/$2")
                .replace(/(\d{4})(\d{1,2})$/, "$1-$2");
        }
    
        return v;
    };

    useEffect(() => {
        setDocument(formatDocument(propValue || ""));
    }, [propValue]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = formatDocument(e.target.value);
        setDocument(value);
        onChange?.({ target: { name: rest.name ?? '', value } });
    };

    return (
        <TextField {...rest}
            label="Documento"
            variant="outlined"
            value={document}
            onChange={handleChange}
            fullWidth
        />
    );
}
