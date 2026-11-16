import { useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { MdDelete, MdRestore, MdCheckCircle } from "react-icons/md";
import CompraAPI from "../../services/compraAPI";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { useAlert } from "../../componentes/Alert/AlertContext";
import style from "./Compra.module.css";

export function Compras() {
    const { mostrarAlerta } = useAlert();

    const [compras, setCompras] = useState([]);
    const [busca, setBusca] = useState("");
    const [ativos, setAtivos] = useState(true);

    useEffect(() => {
        const timer = setTimeout(carregar, 300);
        return () => clearTimeout(timer);
    }, [busca, ativos]);

    async function carregar() {
        try {
            const dados = busca.trim()
                ? await CompraAPI.buscarAsync(busca, ativos)
                : await CompraAPI.listarAsync(ativos);
            setCompras(dados);
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao carregar compras.", "danger");
        }
    }

    async function concluir(id) {
        try {
            await CompraAPI.concluirAsync(id);
            mostrarAlerta("Compra concluída! Estoque atualizado.", "success");
            carregar();
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao concluir.", "danger");
        }
    }

    async function desativar(id) {
        try {
            await CompraAPI.deletarAsync(id);
            carregar();
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao desativar.", "danger");
        }
    }

    async function restaurar(id) {
        try {
            await CompraAPI.restaurarAsync(id);
            carregar();
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao restaurar.", "danger");
        }
    }

    return (
        <Topbar>
            <div className={style.pagina_conteudo}>
                <div className={style.pagina_cabecalho}>
                    <h3>Compras</h3>
                </div>

                <div className={style.barra_opcoes}>
                    <Form.Control
                        className={style.busca}
                        placeholder="Buscar por cliente..."
                        value={busca}
                        onChange={(e) => setBusca(e.target.value)}
                    />
                    <Form.Check
                        type="switch"
                        id="ativos"
                        label="Mostrar ativas"
                        checked={ativos}
                        onChange={(e) => setAtivos(e.target.checked)}
                    />
                </div>

                <div className={style.lista}>
                    {compras.map((c) => (
                        <div key={c.id} className={style.item}>
                            <span className={c.concluido ? style.selo_ok : style.selo_pendente}>
                                {c.concluido ? "Concluída" : "Pendente"}
                            </span>

                            <div className={style.info}>
                                <strong>{c.clienteNome}</strong>
                                <small>
                                    {new Date(c.dataCompra).toLocaleDateString("pt-BR")} · vendedor {c.vendedorNome} · qtd {c.quantidade}
                                </small>
                                <small>{c.produtos.map((p) => p.nome).join(", ")}</small>
                            </div>

                            <span className={style.valor}>R$ {Number(c.valor).toFixed(2)}</span>

                            {c.ativo && !c.concluido && (
                                <button onClick={() => concluir(c.id)} className={style.botao_concluir} title="Concluir">
                                    <MdCheckCircle />
                                </button>
                            )}
                            {c.ativo ? (
                                !c.concluido && (
                                    <button onClick={() => desativar(c.id)} className={style.botao_deletar}>
                                        <MdDelete />
                                    </button>
                                )
                            ) : (
                                <button onClick={() => restaurar(c.id)} className={style.botao_editar}>
                                    <MdRestore />
                                </button>
                            )}
                        </div>
                    ))}
                </div>

                <div className={style.rodape}>
                    <Link to="/compras/novo" className={style.botao_novo}>Nova compra</Link>
                </div>
            </div>
        </Topbar>
    );
}