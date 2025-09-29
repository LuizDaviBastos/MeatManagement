import { useEffect, useState } from "react";
import { TextField, Box, Grid, MenuItem } from "@mui/material";
import { Meat } from "../../types";
import { ButtonSave } from "../../components/ButtonSave";
import { ButtonDelete, ConfirmModal } from "../../components";
import { MeatService } from "../../api/services";
import { useNavigate, useParams } from "react-router-dom";
import { getMeatOrigins } from "../../functions";

export function Carne() {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();

  const [meat, setMeat] = useState<Meat>({
    name: "",
    origin: 0
  });

  const origins = getMeatOrigins();

  const [errors, setErrors] = useState<any>({});
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [alertOpen, setAlertOpen] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");
  const [alertTitle, setAlertTitle] = useState("Erro");

  useEffect(() => {
    if (id) {
      MeatService.getMeatById(id)
        .then(setMeat)
        .catch((error) => {
          setAlertMessage(error?.message);
          setAlertOpen(true);
        });
    }
  }, [id]);

  const handleDelete = async () => {
    if (id) {
      try {
        await MeatService.deleteMeat(id);
        navigate("/carnes");
      } catch (error: any) {
        setAlertMessage(error?.message);
        setAlertOpen(true);
      } finally {
        setConfirmOpen(false);
      }
    }
  };

  const openAlertModal = (title: string, message: string) => {
    setAlertMessage(message);
    setAlertTitle(title);
    setAlertOpen(true);
  }

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    setMeat((prev) => ({ ...prev, [name]: value }));
  };

  const validate = () => {
    const newErrors: any = {};
    if (!meat.name) newErrors.name = "Nome obrigatório";
    if (meat.origin != 0 && !meat.origin) newErrors.origin = "Origem obrigatória";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    if (!validate()) return;

    try {
      if (id) {
        await MeatService.updateMeat(id, meat);
        setAlertOpen(true);
        openAlertModal("Sucesso", "Carne atualizada com sucesso");
      } else {
        await MeatService.createMeat(meat);
        navigate("/carnes");
      }
    } catch (error: any) {
      setAlertMessage(error?.message);
      setAlertOpen(true);
    }

    setErrors({});
  };

  return (
    <div>
      <h1 className="page-title">{id ? "Editar Carne" : "Nova Carne"}</h1>

      <Box
        component="form"
        onSubmit={handleSubmit}
        sx={{
          display: "flex",
          flexDirection: "column",
          gap: 2,
          width: 600,
          marginTop: 6,
          borderRadius: 2,
        }}
      >
        <TextField
          name="name"
          label="Descrição"
          value={meat.name}
          onChange={handleChange}
          error={!!errors.name}
          helperText={errors.name}
          variant="outlined"
        />

        <TextField
          select
          name="origin"
          label="Origem"
          type="number"
          value={meat.origin}
          onChange={handleChange}
          error={!!errors.origin}
          helperText={errors.origin}
          style={{ textAlign: 'left', width: 200 }}
          variant="outlined"
        >
          {origins.map((o) => (
            <MenuItem key={o.id} value={o.id}>
              {o.name}
            </MenuItem>
          ))}
        </TextField>

        <Grid style={{ display: "flex", justifyContent: "space-between" }}>
          <ButtonSave />
          {id && <ButtonDelete onClick={() => setConfirmOpen(true)} />}
        </Grid>
      </Box>

      <ConfirmModal
        open={confirmOpen}
        title="Confirmação"
        message="Deseja realmente excluir esta carne?"
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
