<%@ Page Title="Báo cáo động" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BaoCaoDong._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />

    <div id="main">
        <div class="col-md-3">
            
            <asp:Panel ID="PanelChonBang" runat="server" BackColor="White"  Height="200px">
                <h2>Bảng</h2> 
                <br />
                <asp:CheckBoxList ID="CheckBoxList_Bang" runat="server" OnSelectedIndexChanged="CheckBoxList_Bang_SelectedIndexChanged">
                </asp:CheckBoxList>
                <br />
            </asp:Panel>
           
        </div>
        <div class="col-md-9">
            <div>
                <asp:Panel ID="PanelTruyVan" runat="server" BackColor="White"  Height="221px">
                    <div>
                        <div class="col-md-2">
                            <h3>Tựa đề:</h3>
                        </div>
                        <div class="col-md-10">
                            <br />
                        <asp:TextBox ID="TextBoxTuaDe" runat="server" Width="500px" ></asp:TextBox>
                        </div>
                    </div>
                    
                    <div>
                        <asp:TextBox ID="TextBoxSQL" runat="server" Height="120px" Width="700px" Wrap ="true" TextMode="MultiLine"></asp:TextBox>
                        <br><br />
                        &emsp;
                        <asp:Button ID="BtnThemHang" runat="server" Text="Thêm Hàng" OnClick="BtnThemHang_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnTaoTruyVan" runat="server" Text="TẠO CÂU TRUY VẤN" OnClick="BtnTaoTruyVan_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:button ID="BtnXuatBaoCao" runat="server" Text="XUẤT BÁO CÁO" OnClick="BtnXuatBaoCao_Click" />
                        
                    </div>
                </asp:Panel>
            </div>
            <br />
            <div>
                <asp:Panel ID="PanelThaoTac" runat="server" BackColor="White" Height="200px">
                    <br />
                    <asp:GridView ID="GridView" runat="server" BackColor="White"  BorderColor="#CCCCCC"  BorderWidth="1px" CellPadding="3" HorizontalAlign="Center" Width="925px" >

                        <Columns>
                            <asp:TemplateField HeaderText="Trường" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="NotSet" FooterStyle-HorizontalAlign="NotSet">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownList_Truong" runat="server" >
                                        
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Sắp xếp" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <asp:DropDownList ID="DropDownList_SapXep" runat="server" >
                                        <asp:ListItem Text="Non_Selected" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Tăng dần" Value="ASC"></asp:ListItem>
                                        <asp:ListItem Text="Giảm dần" Value="DESC"></asp:ListItem>
                                        <asp:ListItem Text="Count" Value="Count"></asp:ListItem>
                                        <asp:ListItem Text="Sum" Value="Sum"></asp:ListItem>
                                        <asp:ListItem Text="Min" Value="Min"></asp:ListItem>                                     
                                        <asp:ListItem Text="Max" Value="Max"></asp:ListItem>
                                       <asp:ListItem Text="Group by" Value="Group by"></asp:ListItem>
                                   </asp:DropDownList>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Hiện" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="Checked_HienThi" runat="server" />
                                   </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Điều Kiện" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <asp:TextBox ID="TextBox_DieuKien" runat="server"></asp:TextBox>
                               </ItemTemplate>
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="hoặc" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Wrap="True" ItemStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                                   <asp:TextBox ID="TextBox_Hoac" runat="server"></asp:TextBox>
                               </ItemTemplate>
                           </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                    
                </asp:Panel>
            </div>

        </div>
    </div>
       

</asp:Content>
