import { Routes, Route } from "react-router-dom";
import './App.css';
import { Home } from './pages';
import { Carne, Carnes, Comprador, Compradores, Pedido, Pedidos } from "./components";

function App() {
  return (
    <div className="App">

      <Routes>
        <Route path="/" element={<Home />}>
          <Route path="pedidos/cadastrar" element={<Pedido />} />
          <Route path="pedidos/listar" element={<Pedidos />} />
          <Route path="compradores/cadastrar" element={<Comprador />} />
          <Route path="compradores/listar" element={<Compradores />} />
          <Route path="carnes/cadastrar" element={<Carne />} />
          <Route path="carnes/listar" element={<Carnes />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
