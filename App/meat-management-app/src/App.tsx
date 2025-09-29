import { Routes, Route } from "react-router-dom";
import './App.css';
import { Home } from './pages';
import { Carne, Carnes, Comprador, Compradores, Pedido, Pedidos } from "./pages";

function App() {
  return (
    <div className="App">

      <Routes>
        <Route path="/" element={<Home />}>
          <Route path="/" element={<Pedidos />} />
          <Route path="pedidos/cadastrar" element={<Pedido />} />
          <Route path="pedidos/:id" element={<Pedido />} />
          <Route path="pedidos" element={<Pedidos />} />
          <Route path="compradores/cadastrar" element={<Comprador />} />
          <Route path="compradores/:id" element={<Comprador />} />
          <Route path="compradores" element={<Compradores />} />
          <Route path="carnes/cadastrar" element={<Carne />} />
          <Route path="carnes/:id" element={<Carne />} />
          <Route path="carnes" element={<Carnes />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
