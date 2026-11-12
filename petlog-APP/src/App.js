import logo from './logo.svg';
import './App.css';
import { Login } from './paginas/Login/Login';
import { RotaLogin} from './paginas/Login/RotaLogin';
import { AlertProvider } from './componentes/Alert/AlertContext';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
function App() {
  return (
      <AlertProvider>
        <BrowserRouter>
         <Routes>
            <Route path='/' element={<Login />} />

         </Routes>
        </BrowserRouter>
      </AlertProvider>
  );
}

export default App;
