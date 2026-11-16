import { useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import CompraAPI from "../../services/compraAPI";
import ClienteAPI from "../../services/clienteAPI";
import ProdutoAPI from "../../services/produtoAPI";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { useAlert } from "../../componentes/Alert/AlertContext";
import style from "./NovaCompra.module.css";

export function NovaCompra() {
    const { mostrarAlerta } = useAlert();
    const navigate = useNavigate();

    const [clientes, setClientes] = useState([]);
    const [produtos, setProdutos] = useState([]);
    const [clienteId, setClienteId] = useState("");
    const [produtoIds, setProdutoIds] = useState([]);
    const [quantidade, setQuantidade] = useState(1);
    const [dataCompra, setDataCompra] = useState(new Date().toISOString().slice(0, 10));

    const vendedor = JSON.parse(localStorage.getItem("vendedor"));

    useEffect(() => {
        async function carregar() {
            try {
                setClientes(await ClienteAPI.listarAsync(true));
                setProdutos(await ProdutoAPI.listarAsync(true));
            } catch (erro) {
                mostrarAlerta("Erro ao carregar dados.", "danger");
            }
        }
        carregar();
    }, []);

    function alternarProduto(id) {
        setProdutoIds(
            produtoIds.includes(id) ? produtoIds.filter((p) => p !== id) : [...produtoIds, id]
        );
    }

    const total = produtos
        .filter((p) => produtoIds.includes(p.id))
        .reduce((soma, p) => soma + p.preco, 0) * Number(quantidade || 0);

    async function salvar(e) {
        e.preventDefault();
        try {
            await CompraAPI.criarAsync({
                clienteId,
                vendedorId: vendedor.id,
                produtoIds,
                quantidade,
                dataCompra
            });
            mostrarAlerta("Compra criada!", "success", () => navigate("/compras"));
        } catch (erro) {
        console.log(erro);
        console.log(erro.response);
        mostrarAlerta(erro.response?.data || "Erro ao criar compra.", "danger");
        }
    }

    return (
        <Topbar>
            <div className={style.pagina_conteudo}>
                <h3>Nova compra</h3>

                <Form onSubmit={salvar} className={style.formulario}>
                    <Form.Label>Cliente</Form.Label>
                    <Form.Select value={clienteId} onChange={(e) => setClienteId(e.target.value)} required>
                        <option value="">Selecione</option>
                        {clientes.map((c) => (
                            <option key={c.id} value={c.id}>{c.nome} {c.sobrenome}</option>
                        ))}
                    </Form.Select>

                    <Form.Label className={style.rotulo}>Produtos</Form.Label>
                    {produtos.map((p) => (
                        <Form.Check
                            key={p.id}
                            id={`produto-${p.id}`}
                            label={`${p.nome} - R$ ${Number(p.preco).toFixed(2)} (estoque ${p.quantidadeEstoque})`}
                            checked={produtoIds.includes(p.id)}
                            onChange={() => alternarProduto(p.id)}
                        />
                    ))}

                    <Form.Label className={style.rotulo}>Quantidade</Form.Label>
                    <Form.Control type="number" min="1" value={quantidade} onChange={(e) => setQuantidade(e.target.value)} required />

                    <Form.Label className={style.rotulo}>Data</Form.Label>
                    <Form.Control type="date" max={new Date().toISOString().slice(0, 10)} value={dataCompra} onChange={(e) => setDataCompra(e.target.value)} required />

                    <p className={style.total}>Total estimado: R$ {total.toFixed(2)}</p>

                    <div className={style.formulario_botoes}>
                        <button type="submit" className={style.botao_novo}>Salvar</button>
                    </div>
                </Form>
            </div>
        </Topbar>
    );
}