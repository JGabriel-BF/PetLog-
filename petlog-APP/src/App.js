import './App.css';
import { Login } from './paginas/Login/Login';
import { Home } from './paginas/Home/Home';
import { Produtos } from './paginas/Produtos/Produtos';
import { RotaLogin } from './paginas/Login/RotaLogin';
import { Clientes } from './paginas/Cliente/Cliente';
import { AlertProvider } from './componentes/Alert/AlertContext';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Compras } from './paginas/Compra/Compra';

function App() {
  return (
    <AlertProvider>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Login />} />
          <Route path='/home' element={<RotaLogin><Home /></RotaLogin>} />
          <Route path='/produtos' element={<RotaLogin><Produtos /></RotaLogin>} />
          <Route path='/clientes' element={<RotaLogin><Clientes></Clientes></RotaLogin>} />
          <Route path='/compras' element={<RotaLogin><Compras /> </RotaLogin>} />
        </Routes>
      </BrowserRouter>
    </AlertProvider>
  );
}

export default App;