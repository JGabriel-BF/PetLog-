import { useEffect, useState } from "react";
import { Form, FormCheck } from "react-bootstrap";
import { Link } from "react-router-dom";
import { MdDriveFileRenameOutline, MdDelete, MdRestore } from "react-icons/md";
import ClienteAPI from "../../services/clienteAPI";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { useAlert } from "../../componentes/Alert/AlertContext";
import style from "./Cliente.module.css"

export function Clientes() {
    const { mostrarAlerta } = useAlert();

    const [clientes, setClientes]  = useState([]);
    const [busca, setBusca] = useState ("");
    const [ativos, setAtivo] = useState(true);
    
    useEffect (() => {
        const timer = setTimeout(carregar, 300);
        return () => clearTimeout(timer);
    }, [busca, ativos]);

    async function carregar () {
        try {
            const dados = busca.trim()
            ? await ClienteAPI.buscaAsync(busca, ativos)
            : await ClienteAPI.listarAsync(ativos);
            setClientes(dados);
        }
        catch (erro){
            mostrarAlerta(erro.response?.data || "Erro ao carregar produtos.", "danger");
        }
    }

    async function desativar(id) {
        try {
            await ClienteAPI.deletarAsync(id);
            carregar();
        }
        catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao desativar.", "danger");
        }
    }
    async function restaurar(id) {
        try {
            await ClienteAPI.restaurarAsync(id);
            carregar();
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao restaurar.", "danger");
        }
    }

    return (
        <Topbar>
            <div className={style.pagina_conteudo}>
                <div className={style.pagina_cabecalho}>
                    <h3>Clientes</h3>
                </div>

                <div className={style.barra_opcoes}>
                    <Form.Control
                        className={style.busca}
                        placeholder="Buscar Cliente"
                        value={busca}
                        onChange={(e => setBusca(e.target.value))}
                    />
                    <FormCheck 
                        type="switch"
                        id="ativos"
                        label="Mostrar ativos"
                        checked={ativos}
                        onChange={(e) => setAtivo(e.target.checked)}
                    />
                </div>

                <div className={style.lista}>
                    {clientes.map((c) => (
                        <div key={c.id} className={style.item}>
                            <div className={style.info}>
                                <strong>{c.nome} {c.sobrenome}</strong>
                                <strong>{c.telefone}</strong> 
                            </div>

                            <Link to="/clientes/editar" state={c.id} className={style.botao_editar} >
                                <MdDriveFileRenameOutline />
                            </Link>

                                {c.ativo ? (
                                <button onClick={() => desativar(c.id)} className={style.botao_deletar}>
                                    <MdDelete />
                                </button>

                                 ) : (

                                <button onClick={() => restaurar(c.id)} className={style.botao_editar}>
                                    <MdRestore />
                                </button>
                                )}
                        </div>
                    ))}
                </div>

                <div className={style.rodape}>
                    <Link to="/clientes/novo" className={style.botao_novo}> Novo </Link>
                </div>
            </div>
        </Topbar>
    )
}