   M S B A M L   `   `   `  ����B  >Planner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:PlannerPlanner  5   	       Planner.ArchiveDB   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Planner     5   	     � Title$  	ArchiveDB��5   	   $	��150��6   $	��300��6(   .��5	      �  � RowDefinitions 5
   
   �� 5      $��2* �6   6*   �� 5      $��* �6   6)   .+�5   
   �� -   5         ��Name$ ArchiveNameTextBox��5   W   $��0��6   $
��Left=�6!   $��Center=�6<   $��30��6s   $	��250��6   $��10q�6�   $��20(�6�   �� 5   
   -   5         ��Name$ Placeholder��5       J� IsHitTestVisible .  $#��Enter the name of the archive��6B   $��Center=�6g   $
��Left=�6�   $��12,0,0,0q�6�   $��16(�6�   
���������5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � y� �� Value$ 	Collapsed=�63    �� Triggers 5      �� 5      	 �� Binding	 6&   �� *Text+
 �� ElementName$
 ArchiveNameTextBox�� �� Value  6_   .a�5      ��  � y�$ Visible=�6;   5      5      5      �� 5   
   -   5        $��1��5      $��50��6   $��20��6)   $	��ADD��65   $��5q�6C   6e   5      5      