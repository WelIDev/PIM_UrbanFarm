   M S B A M L   `   `   `  ����  
DesktopApp.    (DesktopApp.Views.Dashboards.ProdutosPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5        N� Title$  Dashboard de Produtos��5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5	      $��* �6   �� 5
      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   ���#a9a9a9$��10q�6C    �� Padding$ 15q�6O    �� CornerRadius$ 8��5   	   $
��Left=�6   $	��300��65   ._�5      �� .+�5      �� $��Produtos Mais Vendidos��6   $��16(�6:   $
��Bold"�6H   ���#3336Z   �� 5      -   5         ��Name$ ProdutosMaisVendidos��5      5      5      �� 5   
   $��1��6   $��0��6!   ���#a9a9a9$��10q�6C   $ 15q�6O   $ 8��6\   ._�5      �� .+�5      �� $��Lançamentos Recentes��6   $��16(�68   $
��Bold"�6F   ���#3336X   �� 5      -   5        $ LancamentosRecentes��5      5      5       �� 5#   
   $��0��6   $��0��6!   ���#a9a9a9$��10q�6C   $ 15q�6O   $ 8��5$   	   $��Right=�6   $	��300��66   ._�5%      �� .+�5&      �� $��Nível de Estoque��6   $��16(�64   $
��Bold"�6B   ���#3336T   �� 5'      -   5        $ NiveisEstoque��5'      5(      5)        OxyPlot.Wpf.PlotView  5,   
   -   5          Name$ GraficoMargemLucro��5,   4   $��0��6   $��1��6'   $	��400��6P   $��10q�6]     5/   
   -   5        $ GraficoEvolucaoVendas��5/   4   $��1��6   $��1��6'   $	��400��6S   $��10q�6`   50      51      