import { HTTPClient } from "./client";

const ProdutoAPI = {
    async obterAsync(produtoId) {
        const response = await HTTPClient.get(`/Produto/Obter/${produtoId}`);
        return response.data;
    },

    async listarAsync(ativo = true) {
        const response = await HTTPClient.get(`/Produto/Listar?ativo=${ativo}`);
        return response.data;
    },

    async buscaAsync(filtro, ativo = true) {
        const response = await HTTPClient.get(
            `/Produto/Busca?filtro=${encodeURIComponent(filtro)}&ativo=${ativo}`);
        return response.data;
    },

    async criarAsync(produto) {
        const response = await HTTPClient.post(`/Produto/Criar`, {
            Nome: produto.nome,
            Preco: Number(produto.preco),
            QuantidadeEstoque: Number(produto.quantidadeEstoque),
            QuantidadeMinima: Number(produto.quantidadeMinima),
            Tipo: Number(produto.tipo)
        });
        return response.data;
    },

    async atualizarAsync(produto) {
        const response = await HTTPClient.put(`/Produto/Atualizar`, {
            Id: produto.id,
            Nome: produto.nome,
            Preco: Number(produto.preco),
            QuantidadeEstoque: Number(produto.quantidadeEstoque),
            QuantidadeMinima: Number(produto.quantidadeMinima),
            Tipo: Number(produto.tipo)
        });
        return response.data;
    },

    async deletarAsync(produtoId) {
        const response = await HTTPClient.delete(`/Produto/Deletar/${produtoId}`);
        return response.data;
    },

    async restaurarAsync(produtoId) {
        const response = await HTTPClient.put(`/Produto/Restaurar/${produtoId}`);
        return response.data;
    }
};

export default ProdutoAPI;