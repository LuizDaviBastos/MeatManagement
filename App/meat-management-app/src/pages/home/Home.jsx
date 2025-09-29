import './Home.css';
import { useState } from 'react';
import { Sidebar, Menu, SubMenu, MenuItem, menuClasses } from 'react-pro-sidebar';
import { FaShoppingCart, FaUser, FaPlus, FaList, FaDrumstickBite, FaClipboardList } from 'react-icons/fa';
import { Outlet, useNavigate } from 'react-router-dom';
import './Home.css';

export function Home() {
  const navigate = useNavigate();
  const [collapsed, setCollapsed] = useState(false);
  const [toggled, setToggled] = useState(false);
  const [broken, setBroken] = useState(true);
  const [rtl, setRtl] = useState(false);
  const [hasImage, setHasImage] = useState(false);
  const [theme, setTheme] = useState('light');

  const themes = {
    light: {
      sidebarBg: '#ffffff',
      sidebarColor: '#607489',
      menuIcon: '#1976d2',
      menuContent: '#ecf3fa',
      hoverBg: '#c5e4ff',
      hoverColor: '#44596e',
      disabledColor: '#9fb6cf',
    },
    dark: {
      sidebarBg: '#0b2948',
      sidebarColor: '#8ba1b7',
      menuIcon: '#59d0ff',
      menuContent: '#082440',
      hoverBg: '#00458b',
      hoverColor: '#b6c8d9',
      disabledColor: '#3e5e7e',
    },
  };

  const hexToRgba = (hex, alpha) => {
    const r = parseInt(hex.slice(1, 3), 16);
    const g = parseInt(hex.slice(3, 5), 16);
    const b = parseInt(hex.slice(5, 7), 16);
    return `rgba(${r}, ${g}, ${b}, ${alpha})`;
  };

  const menuItemStyles = {
    root: { fontSize: '13px', fontWeight: 400 },
    icon: { color: themes[theme].menuIcon, [`&.${menuClasses.disabled}`]: { color: themes[theme].disabledColor } },
    SubMenuExpandIcon: { color: '#b6b7b9' },
    subMenuContent: ({ level }) => ({
      backgroundColor: level === 0 ? hexToRgba(themes[theme].menuContent, hasImage && !collapsed ? 0.4 : 1) : 'transparent',
    }),
    button: {
      [`&.${menuClasses.disabled}`]: { color: themes[theme].disabledColor },
      '&:hover': { backgroundColor: hexToRgba(themes[theme].hoverBg, hasImage ? 0.8 : 1), color: themes[theme].hoverColor },
    },
    label: ({ open }) => ({ fontWeight: open ? 600 : undefined }),
  };

  return (
    <div className="home-container">
      <Sidebar
        collapsed={collapsed}
        toggled={toggled}
        onBackdropClick={() => setToggled(false)}
        onBreakPoint={setBroken}
        rtl={rtl}
        breakPoint="md"
        backgroundColor={hexToRgba(themes[theme].sidebarBg, hasImage ? 0.9 : 1)}
        rootStyles={{ color: themes[theme].sidebarColor, height: '100vh', position: 'fixed' }}
        className="sidebar"
      >
        <div className="sidebar-content">
          <div className="sidebar-header">
            <h3 className="sidebar-title">
              <FaShoppingCart /> <span>Meats</span>
            </h3>
          </div>

          <div className="sidebar-menu">
            <div className="menu-section-title">General</div>
            <Menu menuItemStyles={menuItemStyles}>
              <SubMenu icon={<FaClipboardList />} label="Pedidos">
                <MenuItem icon={<FaPlus />} onClick={() => navigate('pedidos/cadastrar')}>Cadastrar</MenuItem>
                <MenuItem icon={<FaList />} onClick={() => navigate('pedidos')}>Listar</MenuItem>
              </SubMenu>

              <SubMenu icon={<FaUser />} label="Compradores">
                <MenuItem icon={<FaPlus />} onClick={() => navigate('compradores/cadastrar')}>Cadastrar</MenuItem>
                <MenuItem icon={<FaList />} onClick={() => navigate('compradores')}>Listar</MenuItem>
              </SubMenu>

              <SubMenu icon={<FaDrumstickBite />} label="Carnes">
                <MenuItem icon={<FaPlus />} onClick={() => navigate('carnes/cadastrar')}>Cadastrar</MenuItem>
                <MenuItem icon={<FaList />} onClick={() => navigate('carnes')}>Listar</MenuItem>
              </SubMenu>
            </Menu>
          </div>
        </div>
      </Sidebar>

      <main className="main-content">
        <div className="main-inner">
          <Outlet />
        </div>
      </main>
    </div>
  );
}
