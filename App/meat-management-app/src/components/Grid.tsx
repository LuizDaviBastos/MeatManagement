import { DataGrid, GridColDef } from '@mui/x-data-grid';

const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 70 },
  { field: 'name', headerName: 'Nome', width: 200 },
  { field: 'age', headerName: 'Idade', type: 'number', width: 100 },
];

const rows = [
  { id: 1, name: 'Luiz Bastos', age: 30 },
  { id: 2, name: 'João Silva', age: 25 },
  { id: 3, name: 'Maria Souza', age: 28 },
];

export function Grid() {
  return <div style={{ height: 400, width: '100%' }}>
    <DataGrid rows={rows} columns={columns} />
  </div>;
}