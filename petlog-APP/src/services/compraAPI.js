import { HTTPClient } from "./client";

const CompraAPI = {

    async obterAsync (compraId) {
        const response = await HTTPClient.get (`/Compra/Obter/${compraId}`);
        return response.data;
    },

    async listarAsync(ativo = true) {
        const response = await HTTPClient.get(`/Compra/Listar?ativo=${ativo}`);
        return response.data;
    },

    async buscaAsync (filtro, ativo = true) {
        const response = await HTTPClient.get (`/Compra/Busca?filtro=${encodeURIComponent(filtro)}&ativo=${ativo}`);
        return response.data;
    },

async criarAsync (compra) {
    const response = await HTTPClient.post(`/Compra/Criar`, {
        ClienteId: Number(compra.clienteId),
        VendedorId: Number(compra.vendedorId),
        ProdutoIds: (compra.produtoIds || []).map(Number),
        Quantidade: Number(compra.quantidade),
        DataCompra: compra.dataCompra
    });
    return response.data;
},

async atualizarAsync(compra) {
    const response = await HTTPClient.put(`/Compra/Atualizar`, {
        Id: compra.id,
        ClienteId: Number(compra.clienteId),
        VendedorId: Number(compra.vendedorId),
        ProdutoIds: (compra.produtoIds || []).map(Number),
        Quantidade: Number(compra.quantidade),
        DataCompra: compra.dataCompra
    });
    return response.data;
},

    async concluirAsync(compraId) {
        const response = await HTTPClient.put(`/Compra/Concluir/${compraId}`);
        return response.data;
    },

    async deletarAsync(compraId) {
        const response = await HTTPClient.delete(`/Compra/Deletar/${compraId}`);
        return response.data;
    },

    async restaurarAsync(compraId) {
        const response = await HTTPClient.put(`/Compra/Restaurar/${compraId}`);
        return response.data;
    }

};

export default CompraAPI;