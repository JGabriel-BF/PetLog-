import { useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { MdEdit, MdDelete, MdRestore } from "react-icons/md";
import ProdutoAPI from "../../services/produtoAPI";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { useAlert } from "../../componentes/Alert/AlertContext";
import style from "./Produtos.module.css";

const TIPOS = ["Ração", "Coleira", "Pote", "Roupa"];

export function Produtos() {
    const { mostrarAlerta } = useAlert();

    const [produtos, setProdutos] = useState([]);
    const [busca, setBusca] = useState("");
    const [ativos, setAtivos] = useState(true);

    useEffect(() => {
        const timer = setTimeout(carregar, 300);
        return () => clearTimeout(timer);
    }, [busca, ativos]);

    async function carregar() {
        try {
            const dados = busca.trim()
                ? await ProdutoAPI.buscaAsync(busca, ativos)
                : await ProdutoAPI.listarAsync(ativos);
            setProdutos(dados);
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao carregar produtos.", "danger");
        }
    }

    async function desativar(id) {
        try {
            await ProdutoAPI.deletarAsync(id);
            carregar();
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao desativar.", "danger");
        }
    }

    async function restaurar(id) {
        try {
            await ProdutoAPI.restaurarAsync(id);
            carregar();
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao restaurar.", "danger");
        }
    }

    return (
        <Topbar>
            <div className={style.pagina_conteudo}>
                <div className={style.pagina_cabecalho}>
                    <h3>Produtos</h3>
                </div>

                <div className={style.barra_opcoes}>
                    <Form.Control
                        className={style.busca}
                        placeholder="Buscar produto..."
                        value={busca}
                        onChange={(e) => setBusca(e.target.value)}
                    />
                </div>

                <div className={style.lista}>
                    {produtos.map((p) => {
                        const baixo = p.quantidadeEstoque <= p.quantidadeMinima;
                        return (
                            <div key={p.id} className={style.item}>
                                <span className={baixo ? style.ponto_baixo : style.ponto_ok} />

                                <div className={style.info}>
                                    <strong>{p.nome}</strong>
                                    <small>{TIPOS[p.tipo]} · estoque {p.quantidadeEstoque}</small>
                                </div>

                                <span className={style.preco}>R$ {Number(p.preco).toFixed(2)}</span>

                                <Link to="/produtos/editar" state={p.id} className={style.botao_editar}>
                                    <MdEdit />
                                </Link>
                                {p.ativo ? (
                                    <button onClick={() => desativar(p.id)} className={style.botao_deletar}>
                                        <MdDelete />
                                    </button>
                                ) : (
                                    <button onClick={() => restaurar(p.id)} className={style.botao_editar}>
                                        <MdRestore />
                                    </button>
                                )}
                            </div>
                        );
                    })}
                </div>

                <div className={style.rodape}>
                    <Link to="/produtos/novo" className={style.botao_novo}>Novo</Link>
                </div>
            </div>
        </Topbar>
    );
}