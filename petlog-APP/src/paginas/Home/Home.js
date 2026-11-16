import { useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import ClienteAPI from "../../services/clienteAPI";
import SugestaoAPI from "../../services/sugestaoAPI";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { useAlert } from "../../componentes/Alert/AlertContext";
import style from "./Home.module.css";

export function Home() {
    const { mostrarAlerta } = useAlert();

    const [clientes, setClientes] = useState([]);
    const [clienteId, setClienteId] = useState("");
    const [sugestao, setSugestao] = useState("");
    const [carregando, setCarregando] = useState(false);

    useEffect(() => {
        async function carregarClientes() {
            try {
                setClientes(await ClienteAPI.listarAsync(true));
            } catch (erro) {
                mostrarAlerta("Erro ao carregar clientes.", "danger");
            }
        }
        carregarClientes();
    }, []);

    async function gerar() {
        if (!clienteId) {
            mostrarAlerta("Selecione um cliente.", "warning");
            return;
        }
        try {
            setCarregando(true);
            setSugestao("");
            const resposta = await SugestaoAPI.gerarParaClienteAsync(clienteId);
            setSugestao(resposta.sugestao);
        } catch (erro) {
            mostrarAlerta(erro.response?.data || "Erro ao gerar sugestão.", "danger");
        } finally {
            setCarregando(false);
        }
    }

    return (
        <Topbar>
            <div className={style.pagina_conteudo}>
                <h3>Sugestão de compra com IA</h3>
                <p>Escolha um cliente e a IA analisa o histórico de compras e o estoque.</p>

                <div className={style.barra}>
                    <Form.Select
                        className={style.select}
                        value={clienteId}
                        onChange={(e) => setClienteId(e.target.value)}
                    >
                        <option value="">Selecione um cliente</option>
                        {clientes.map((c) => (
                            <option key={c.id} value={c.id}>{c.nome} {c.sobrenome}</option>
                        ))}
                    </Form.Select>

                    <button onClick={gerar} disabled={carregando} className={style.botao_ia}>
                        {carregando ? "Analisando..." : "Gerar sugestão"}
                    </button>
                </div>

                {sugestao && <div className={style.resultado}>{sugestao}</div>}
            </div>
        </Topbar>
    );
}