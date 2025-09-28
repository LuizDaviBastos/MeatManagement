import { DataGrid, GridColDef } from "@mui/x-data-grid";

interface GridProps {
  columns: GridColDef[];
  rows: any[];
  height?: number;
}

export function Grid({ columns, rows, height = 400 }: GridProps) {
  return (
    <div style={{ height, width: "100%" }}>
      <DataGrid rows={rows} columns={columns} />
    </div>
  );
}