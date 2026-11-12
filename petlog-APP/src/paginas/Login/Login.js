import style from "./Login.module.css"
import { useState } from "react";
import LoginAPI from "../../services/loginAPI";
import { useNavigate } from "react-router-dom";
import Logo from "../../assets/logoComTituloPetLog.png";
import { useAlert } from "../../componentes/Alert/AlertContext";
import { Form } from "react-bootstrap";

export function Login ()
{
    const { mostrarAlerta } = useAlert(); 

    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');

    const navigate = useNavigate();

    const isFormValid = () => {
        return email.length > 5 && senha.length >=6;
    };

    async function handleLogin(e)
    {
        e.preventDefault();

        if(!isFormValid()){
            mostrarAlerta("Insira um e-mail válido e uma senha com no mínimo 6 caracteres.", "warning");
            return;
        }

        try 
        {
            const response = await LoginAPI.loginAsync(email, senha);

            if (response?.status === 200) {
                localStorage.setItem("usuario",JSON.stringify(response.data));
                mostrarAlerta(`Bem-Vindo, ${response.data.nome}!`, 'success', () => {
                    navigate("/dashboard");
                });
            }
        }
        
        catch (error)
        {
            if (error.response?.status === 400)
            {
                mostrarAlerta("Preencha e-mail e senha corretamente.", "danger");
                setEmail("");
                setSenha("");
            }
            else if (error.response?.status === 401) 
            {
                mostrarAlerta("E-mail ou senha inválidos, tente novamente.", "danger");
                setSenha("");
            }
            else
            {
                mostrarAlerta("Erro ao fazer login.", "danger");
            }
        }
    }

    return (
        <div className={style.login_conteudo}>
            <img src={Logo} alt="Logo-PetLog" className={style.logo} />

            <Form onSubmit={handleLogin} className={style.formulario}>

                <Form.Group controlId="formEmail" className="mb-3">
                    <Form.Label className={style.rotulo}>E-mail</Form.Label>
                    <Form.Control
                        className={style.campo}
                        type="email"
                        placeholder="Digite seu e-mail"
                        name="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="formSenha" className="mb-3">
                    <Form.Label className={style.rotulo}>Senha</Form.Label>
                    <Form.Control
                        className={style.campo}
                        type="password"
                        placeholder="******"
                        name="senha"
                        value={senha}
                        onChange={(e) => setSenha(e.target.value)}
                        required
                    />
                </Form.Group>

                <button type="submit" className={style.botao_novo}>Entrar</button>
            </Form>
        </div>
    )
}