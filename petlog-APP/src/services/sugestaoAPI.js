import { HTTPClient } from "./client"

const SugestaoAPI = {
    async gerarParaClienteAsync(clienteId) {
        const response = await HTTPClient.get(`/Sugestao/Cliente/${clienteId}`);
        return response.data;
    }
};

export default SugestaoAPI;