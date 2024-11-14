let carrinho = [];
let total = 0;
let compraAtual = null;

// Carregar produtos da API
async function carregarProdutos() {
    try {
        const response = await fetch('/Pedido/ObterProdutos/');
        const produtos = await response.json();

        const produtosList = document.getElementById('produtos');
        produtos.forEach((produto, index) => {
            const li = document.createElement('li');
            li.innerHTML = `
                ${produto.nome} - R$ ${produto.preco.toFixed(2)} 
                <div>
                    <button onclick="addItem(${index})">+</button>
                    <span id="quantidade-${index}" class="quantidade">0</span>
                    <button onclick="removeItem(${index})">-</button>
                </div>
            `;
            produtosList.appendChild(li);
        });
    } catch (error) {
        console.error('Erro ao carregar os produtos:', error);
    }
}

window.onload = function() {
    carregarProdutos();
};

// Funções para manipulação do carrinho

function addItem(index) {
    const produto = produtos[index];
    const quantidadeSpan = document.getElementById(`quantidade-${index}`);
    const quantidade = parseInt(quantidadeSpan.innerText) + 1;
    quantidadeSpan.innerText = quantidade;

    const itemExistente = carrinho.find(item => item.nome === produto.nome);
    if (itemExistente) {
        itemExistente.quantidade++;
    } else {
        carrinho.push({ nome: produto.nome, preco: produto.preco, quantidade: 1 });
    }

    total += produto.preco;
    updateTotal();
    updatePrevisaoList();
}

function removeItem(index) {
    const produto = produtos[index];
    const quantidadeSpan = document.getElementById(`quantidade-${index}`);
    const quantidade = parseInt(quantidadeSpan.innerText);

    if (quantidade > 0) {
        quantidadeSpan.innerText = quantidade - 1;

        const itemExistente = carrinho.find(item => item.nome === produto.nome);
        if (itemExistente) {
            itemExistente.quantidade--;
            total -= produto.preco;

            if (itemExistente.quantidade === 0) {
                carrinho = carrinho.filter(item => item.nome !== produto.nome);
            }
        }

        updateTotal();
        updatePrevisaoList();
    }
}

function updateTotal() {
    document.getElementById('total').innerText = total.toFixed(2);
}

function updatePrevisaoList() {
    const previsaoList = document.getElementById('previsao-list');
    previsaoList.innerHTML = '';

    carrinho.forEach(item => {
        const li = document.createElement('li');
        li.innerText = `${item.nome} - R$ ${item.preco.toFixed(2)} (x${item.quantidade})`;
        previsaoList.appendChild(li);
    });
}

// Função para finalizar a compra
function finalizarCompra() {
    // Lógica para finalizar a compra
    alert('Compra finalizada!'); // Placeholder para a lógica de finalização
}
