   M S B A M L   `   `   `  ����  
DesktopApp4    .DesktopApp.Views.Dashboards.EntradasSaidasPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5        N� Title$1  +Dashboard de Entradas e Saídas Monetárias��5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��10q�6"   .+�5      �� $��Visão Geral��6   $��18(�6+   $
��Bold"�69   $��0,0,0,10q�6K   �� 5      ���#a9a9a9 �� Padding$ 10q�6*    �� CornerRadius$ 8��67   $��5q�6H   ._�5      �� .+�5      �� -   5         ��Name$ TotalEntradas��5       $ ��Total de Entradas: R$ 0,00��67   $��16(�6Y   $
��Bold"�6g   ���#333$��0,0,0,5q�6�   �� 5      -   5        $ TotalSaidas��5       $��Total de Saídas: R$ 0,00��65   $��16(�6U   $
��Bold"�6c   ���#333$��0,0,0,5q�6�   �� 5      -   5        $ Balanco��5       $��Balanço: R$ 0,00��61   $��16(�6I   $
��Bold"�6W   ���#3336i   5      5      5      � 5   
   $��1��6   $��10q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5"      �� $��0��6   $��5q�6)   .+�5#      �� $��Detalhes Abastecimentos��6   $��18(�6;   $
��Bold"�6I   $��0,0,0,10q�6[   �� 5$      ���#a9a9a9$ 10q�6.   $ 8��6;   $��0,0,0,10q�6L   ._�5%      �� -   5         ��Name$ DetalhesEntradas��5%   #   5&      5'      5(      �� 5+   
   $��2��6   $��5q�6"   .+�5,      �� $��Análise Gráfica��6   $��18(�6/   $
��Bold"�6=   $��0,0,0,10q�6O   �� 5-      ���#a9a9a9$ 10q�6*   $ 8��67   $��0,0,0,10q�6H   ._�5.        OxyPlot.Wpf.PlotView  -   5          Name$ GraficoEntradasSaidas��5.      $	��300��6>   5/      �� 51      $��Exportar��6   $��Right=�6(   $��Bottom=�52      $��0,10,5,0q�53         MostrarVendasButtonStyle#��[  6'   $	��100��6Y   54      55      56      