const profileForm = document.getElementById('profileForm');
const submitButton = document.getElementById('submitButton');
let editingRow = null;

profileForm.addEventListener('submit', function (event) {
    event.preventDefault();  // Previne o envio do formulário, que redirecionaria a página

    const nome = document.getElementById('Nome').value;
    const email = document.getElementById('Email').value;
    const senha = document.getElementById('Senha').value;
    const funcao = document.getElementById('Funcao').value;

    // Cria o objeto para enviar para a API
    const usuario = {
        nome: nome,
        email: email,
        senha: senha,
        funcao: parseInt(funcao)
    };

    // Envia os dados para a API
    fetch('https://localhost:7124/api/Usuario/InserirUsuario', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(usuario),
    })
        .then(response => response.text())  // Captura a resposta como texto
        .then(text => {
            console.log('Resposta da API:', text);

            // Verifica se a resposta contém o texto de sucesso
            if (text === "Usuario gravado com sucesso!") {
                console.log('Usuário adicionado com sucesso');

                // Após o sucesso, redireciona para a página de "Gerenciamento de Perfis"
                window.location.href = 'GerenciamentoPerfis'
                console.error('Erro ao adicionar usuário:', text);
            }
        })
        .catch(error => {
            console.error('Erro na requisição:', error);
        });
});


// Função para editar um perfil


function excluirUsuario(button, usuarioId) {
    // Encontrar a linha (tr) mais próxima do botão
    const row = button.closest('tr');

    // Verifica se a linha foi encontrada
    if (!row) {
        console.error("Não foi possível encontrar a linha");
        return;
    }

    // Envia a requisição para a API para excluir o usuário
    fetch(`https://localhost:7124/api/Usuario/ExcluirUsuario/?id=${usuarioId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => {
            if (response.ok) {
                // Se a requisição for bem-sucedida, remova a linha da tabela
                row.remove(); // Usar row.remove() para remover a linha
                showNotification('delete'); // Exibir notificação de sucesso
            } else {
                console.error('Erro ao excluir o usuário');
                alert('Erro ao excluir o usuário');
            }
        })
        .catch(error => {
            console.error('Erro na requisição:', error);
            alert('Erro ao excluir o usuário');
        });
}

// Função para pesquisar perfis
function pesquisarPerfil() {
    const filter = searchInput.value.toUpperCase();
    const selectedColumn = filterColumn.value;
    const rows = profileTableBody.getElementsByTagName('tr');

    for (let i = 0; i < rows.length; i++) {
        const cells = rows[i].getElementsByTagName('td');
        let match = false;

        switch (selectedColumn) {
            case 'Id':
                match = cells[0].textContent.toUpperCase().indexOf(filter) > -1;
                break;
            case 'Nome':
                match = cells[1].textContent.toUpperCase().indexOf(filter) > -1;
                break;
            case 'Email':
                match = cells[2].textContent.toUpperCase().indexOf(filter) > -1;
                break;
            case 'Funcao':
                match = cells[3].textContent.toUpperCase().indexOf(filter) > -1;
                break;
            case 'data':
                match = cells[4].textContent.toUpperCase().indexOf(filter) > -1;
                break;
        }

        // Mostrar ou esconder a linha com base na correspondência
        rows[i].style.display = match ? '' : 'none';
    }
}

// Função para mostrar uma aba
function showTab(tabId) {
    const tabs = document.querySelectorAll('.tab');
    tabs.forEach(tab => {
        if (tab.id === tabId) {
            tab.style.display = 'block';
        } else {
            tab.style.display = 'none';
        }
    });

    const buttons = document.querySelectorAll('.tab-button');
    buttons.forEach(button => {
        button.classList.toggle('active', button.getAttribute('onclick').includes(tabId));
    });
}

// Função para gerar ID aleatório
function gerarID() {
    const caracteres = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    let resultado = '';
    for (let i = 0; i < 5; i++) {
        resultado += caracteres.charAt(Math.floor(Math.random() * caracteres.length));
    }
    return resultado;
}

// Função para obter a data atual no formato DD/MM/AAAA
function obterDataAtual() {
    const hoje = new Date();
    const dia = String(hoje.getDate()).padStart(2, '0');
    const mes = String(hoje.getMonth() + 1).padStart(2, '0');
    const ano = hoje.getFullYear();
    return `${dia}/${mes}/${ano}`;
}

// Função para mostrar a notificação
function showNotification(type) {
    let message = '';
    switch (type) {
        case 'success':
            message = 'Perfil adicionado com sucesso!';
            break;
        case 'edit':
            message = 'Perfil editado com sucesso!';
            break;
        case 'delete':
            message = 'Perfil excluído com sucesso!';
            break;
        default:
            message = 'Ação realizada com sucesso!';
            break;
    }
    alert(message);
}

// Função para exibir a barra lateral
function toggleSidebar() {
    const sidebar = document.querySelector('.sidebar');
    const mainContent = document.querySelector('.main-content');

    if (sidebar.style.left === '0px') {
        sidebar.style.left = '-250px'; // Esconder a sidebar
        mainContent.style.marginLeft = '0'; // Ajustar o conteúdo principal
    } else {
        sidebar.style.left = '0'; // Mostrar a sidebar
        mainContent.style.marginLeft = '250px'; // Ajustar o conteúdo principal
    }
}

// Fechar o menu ao clicar fora dele
window.onclick = function (event) {
    const sidebar = document.querySelector('.sidebar');
    if (!event.target.matches('.menu-icon') && sidebar.style.left === '0px') {
        sidebar.style.left = '-250px'; // Esconder a sidebar
        document.querySelector('.main-content').style.marginLeft = '0'; // Ajustar o conteúdo principal
    }
};

// Itens do perfil
function toggleProfileMenu(event) {
    const profileMenu = document.querySelector('.profile-menu');

    // Alterna a visibilidade do menu de perfil
    profileMenu.style.display = profileMenu.style.display === 'block' ? 'none' : 'block';
    event.stopPropagation(); // Impede a propagação do clique para a janela
}
