import openai
import sys

# Defina sua chave de API
openai.api_key = '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'

# Função para enviar uma solicitação à API do ChatGPT
def enviar_solicitacao(pergunta, conversa):
    resposta = openai.Completion.create(
        engine='text-davinci-003',  # Especifique o mecanismo de geração de texto
        prompt=conversa + 'Pergunta: ' + pergunta + '\nResposta:',  # Concatena a pergunta à conversa
        max_tokens=50,  # Define o limite máximo de tokens na resposta
        temperature=0.7,  # Controla a aleatoriedade da resposta (0.0 para respostas mais determinísticas)
        n=1,  # Especifica o número de respostas a serem retornadas
        stop=None,  # Define uma sequência opcional para parar a geração de texto
    )
    return resposta.choices[0].text.strip()

# Exemplo de uso
conversa = "extrair dados"
pergunta = "dado o texto, retorne apenas o nome, cpf (no texto é algo nessa formatação 000.000.000-00), nome da mae, data (no texto, encontra-se proximo ao texto 'nascimento' e tem o formato 00/00/0000, quero esse formato completo):"
pergunta += "retorne os valores separado por , tudo em uma unica linha, sem formatações"
pergunta += "\n\n"
pergunta += sys.argv[1]

resposta = enviar_solicitacao(pergunta, conversa)
print(resposta)