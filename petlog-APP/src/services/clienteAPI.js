import { HTTPClient } from "./client";

const ClienteAPI = {
    async obterAsync(clienteId) {
        const response = await HTTPClient.get(`/Cliente/Obter/${clienteId}`);
        return response.data;
    },

    async listarAsync(ativo = true) {
        const response = await HTTPClient.get(`/Cliente/Listar?ativo=${ativo}`);
        return response.data;
    },

    async buscaAsync(filtro, ativo = true) {
        const response = await HTTPClient.get(
            `/Cliente/Busca?filtro=${encodeURIComponent(filtro)}&ativo=${ativo}`);
        return response.data;
    },

    async criarAsync(cliente) {
        const response = await HTTPClient.post(`/Cliente/Criar`, {
            Nome: cliente.nome,
            Sobrenome: cliente.sobrenome,
            Telefone: Number(cliente.telefone)
        });
        return response.data;
    },

    async atualizarAsync(cliente) {
        const response = await HTTPClient.put(`/Cliente/Atualizar`, {
            Id: cliente.id,
            Nome: cliente.nome,
            Sobrenome: cliente.sobrenome,
            Telefone: Number(cliente.telefone)
        });
        return response.data;
    },

    async deletarAsync(clienteId) {
        const response = await HTTPClient.delete(`/Cliente/Deletar/${clienteId}`);
        return response.data;
    },

    async restaurarAsync(clienteId) {
        const response = await HTTPClient.put(`/Cliente/Restaurar/${clienteId}`);
        return response.data;
    }
};

export default ClienteAPI;