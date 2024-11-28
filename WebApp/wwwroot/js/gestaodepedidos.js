let produtos = [];
let carrinho = [];
let total = 0;
let compraAtual = null;

document.getElementById('tab1').addEventListener('click', function () {
    showTab('carrinho');
});

document.getElementById('tab2').addEventListener('click', function () {
    showTab('visualizacao');
    atualizarTabelaVisualizacao();
});
document.addEventListener('DOMContentLoaded', function () {
    carregarProdutos();
    showTab('visualizacao');
});

async function carregarProdutos() {
    try {
        const response = await fetch('https://localhost:7124/api/Produto/ObterProdutos');
        if (!response.ok) {
            throw new Error(`Erro ao carregar produtos: ${response.statusText}`);
        }
        
        const data = await response.json();
        produtos = data.$values;

        console.log(produtos);

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
        console.error('Erro ao carregar produtos:', error);
    }
}


function gerarIdAleatorio() {
    return Math.random().toString(36).substr(2, 5).toUpperCase();
}

function finalizarCompra() {
    const visualizacaoTabelaBody = document.querySelector('#visualizacao-tabela tbody');

    if (carrinho.length === 0) {
        alert('Adicione itens ao carrinho antes de finalizar a compra.');
        return;
    }

    if (compraAtual) {
        const linhaParaEditar = visualizacaoTabelaBody.querySelector(`tr[data-id="${compraAtual}"]`);
        if (linhaParaEditar) {
            const itensComprados = carrinho.map(item => `${item.nome} (x${item.quantidade})`).join(', ');
            linhaParaEditar.cells[2].innerText = itensComprados;
            total = carrinho.reduce((acc, item) => acc + (item.preco * item.quantidade), 0);
            linhaParaEditar.cells[3].innerText = `R$ ${total.toFixed(2)}`;
            updateTotal();
            updatePrevisaoList();

            compraAtual = null;
            restaurarBotaoFinalizar();
            limparCampos();
            return;
        }
    }

    const compraId = gerarIdAleatorio();
    const dataCompra = new Date().toLocaleDateString('pt-BR');
    const itensComprados = carrinho.map(item => `${item.nome} (x${item.quantidade})`).join(', ');
    const valorTotal = total.toFixed(2);

    const novaLinha = document.createElement('tr');
    novaLinha.innerHTML = `
        <td>#${compraId}</td>
        <td>${dataCompra}</td>
        <td>${itensComprados}</td>
        <td>R$ ${valorTotal}</td>
        <td>
            <button class="acao-button editar" onclick="editarCompra('${compraId}')">Editar</button>
            <button class="acao-button excluir" onclick="excluirCompra('${compraId}')">Excluir</button>
        </td>
    `;
    novaLinha.setAttribute('data-id', compraId);
    visualizacaoTabelaBody.appendChild(novaLinha);

    carrinho = [];
    total = 0;
    updateTotal();
    updatePrevisaoList();

    produtos.forEach((_, index) => {
        document.getElementById(`quantidade-${index}`).innerText = '0';
    });

    restaurarBotaoFinalizar();
    showNotification('success');
}

function editarCompra(id) {
    const visualizacaoTabelaBody = document.querySelector('#visualizacao-tabela tbody');
    const linhaParaEditar = visualizacaoTabelaBody.querySelector(`tr[data-id="${id}"]`);

    if (!linhaParaEditar) {
        alert(`Compra com ID: ${id} não encontrada.`);
        return;
    }

    const itens = linhaParaEditar.cells[2].innerText.split(', ');

    carrinho = [];

    itens.forEach(item => {
        const [nome, quantidadeStr] = item.split(' (x');
        const quantidade = parseInt(quantidadeStr);
        const produto = produtos.find(p => p.nome === nome);

        if (produto) {
            carrinho.push({nome: produto.nome, preco: produto.preco, quantidade});
            const index = produtos.indexOf(produto);
            document.getElementById(`quantidade-${index}`).innerText = quantidade;
        }
    });

    total = carrinho.reduce((acc, item) => acc + (item.preco * item.quantidade), 0);
    updateTotal();
    updatePrevisaoList();

    let finalizarButton = document.getElementById('finalizar');
    finalizarButton.innerText = 'Editar Compra';
    finalizarButton.style.backgroundColor = '#ffc107';

    compraAtual = id;
    showTab('carrinho');
}

function restaurarBotaoFinalizar() {
    const finalizarButton = document.getElementById('finalizar');
    finalizarButton.innerText = 'Finalizar Compra';
    finalizarButton.style.backgroundColor = '#28a745';
}

function limparCampos() {
    carrinho = [];
    total = 0;
    updateTotal();
    updatePrevisaoList();

    produtos.forEach((_, index) => {
        document.getElementById(`quantidade-${index}`).innerText = '0';
    });
}

function excluirCompra(id) {
    const visualizacaoTabelaBody = document.querySelector('#visualizacao-tabela tbody');
    const linhaParaRemover = visualizacaoTabelaBody.querySelector(`tr[data-id="${id}"]`);
    if (linhaParaRemover) {
        visualizacaoTabelaBody.removeChild(linhaParaRemover);
        alert(`Compra com ID: ${id} excluída com sucesso.`);
    } else {
        alert(`Compra com ID: ${id} não encontrada.`);
    }
}

function addItem(index) {
    const produto = produtos[index];
    const quantidadeSpan = document.getElementById(`quantidade-${index}`);

    const quantidade = parseInt(quantidadeSpan.innerText) + 1;
    quantidadeSpan.innerText = quantidade;

    const itemExistente = carrinho.find(item => item.nome === produto.nome);
    if (itemExistente) {
        itemExistente.quantidade++;
    } else {
        carrinho.push({nome: produto.nome, preco: produto.preco, quantidade: 1});
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
        li.innerText = `${item.nome} (x${item.quantidade}) - R$ ${(item.preco * item.quantidade).toFixed(2)}`;
        previsaoList.appendChild(li);
    });
}

function showNotification(type) {
    let notification;
    switch (type) {
        case 'success':
            notification = successNotification;
            break;
        case 'edit':
            notification = editNotification;
            break;
        case 'delete':
            notification = deleteNotification;
            break;
        default:
            return;
    }
    notification.classList.remove('hidden');
    notification.classList.add('visible');

    setTimeout(function () {
        notification.classList.remove('visible');
        notification.classList.add('hidden');
    }, 3000);
}
