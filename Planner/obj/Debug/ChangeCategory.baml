   M S B A M L   `   `   `  ����B  >Planner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:PlannerPlanner  5   	       Planner.ChangeCategory   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Planner     5   	     � Title$  ChangeCategory��5   	   $	��200��6    $	��430��6-   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   6)   �� 5      $��* �6   6)   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $	��330 �6#   61   �� 5      $��* �6#   6/   .+�5      �� -   5         ��Name$ TaskCategoryTextBox��5   ^   $��0��6   $
��Left=�6(   $��Center=�6C   $��30��6{   $	��300��6�   $��10q�6�   $��20(�6�   �� 5      -   5         ��Name$ Placeholder��5       J� IsHitTestVisible .  $%��Enter a new category (Optional)��6F   $��Center=�6m   $
��Left=�6�   $��12,0,0,0q�6�   $��16(�6�   
���������5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � y� �� Value$ 	Collapsed=�67   	 �� Triggers	 5      �� 5      
 �� Binding
 6*   �� *Text+ �� ElementName$ TaskCategoryTextBox�� �� Value  6d   .a�5   "   ��  � y�$ Visible=�6?   5      5      5      �� 5       -   5         ��Name$ 	AddButton��5       $��1��6(   $��80��68   $��30��6C   $
��Left=�6O   $	��Add��6j   6�   5!      � 5"   
   $��1��6    5#      �� 5$      $	��330 �6#   61   �� 5%      $��* �6#   6/   .+�5'      �� -   5        $��Category��5'      $��30��6(   $	��300��64   $��10q�6@    �� SelectedIndex$ 0��6L   $
��Left=�6^   .@�5(      �� .��6   
Minor Task6+   �� 5)      .��6   
Major Task6+   5*      �� 5+      -   5        $ Change��5+   %   $��1��6   $
��Left=�65   $��80��6P   $��30��6[   $��Change��6g   6�   5,      5.      5/      