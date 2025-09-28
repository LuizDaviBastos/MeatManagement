import React from 'react';
import { menuClasses, Sidebar, Menu, SubMenu, MenuItem } from 'react-pro-sidebar';
import { FaShoppingCart, FaUser, FaPlus, FaList, FaDrumstickBite, FaClipboardList, FaDesktop } from 'react-icons/fa';
import { Outlet, useNavigate } from 'react-router-dom';


export function Home() {
    const navigate = useNavigate();
    const [collapsed, setCollapsed] = React.useState(false);
    const [toggled, setToggled] = React.useState(false);
    const [broken, setBroken] = React.useState(true);
    const [rtl, setRtl] = React.useState(false);
    const [hasImage, setHasImage] = React.useState(false);
    const [theme, setTheme] = React.useState('light');

    const themes = {
        light: {
            sidebar: {
                backgroundColor: '#ffffff',
                color: '#607489',
            },
            menu: {
                menuContent: '#ecf3fa',
                icon: '#0098e5',
                hover: {
                    backgroundColor: '#c5e4ff',
                    color: '#44596e',
                },
                disabled: {
                    color: '#9fb6cf',
                },
            },
        },
        dark: {
            sidebar: {
                backgroundColor: '#0b2948',
                color: '#8ba1b7',
            },
            menu: {
                menuContent: '#082440',
                icon: '#59d0ff',
                hover: {
                    backgroundColor: '#00458b',
                    color: '#b6c8d9',
                },
                disabled: {
                    color: '#3e5e7e',
                },
            },
        },
    };
    const hexToRgba = (hex, alpha) => {
        const r = parseInt(hex.slice(1, 3), 16);
        const g = parseInt(hex.slice(3, 5), 16);
        const b = parseInt(hex.slice(5, 7), 16);

        return `rgba(${r}, ${g}, ${b}, ${alpha})`;
    };
    const menuItemStyles = {
        root: {
            fontSize: '13px',
            fontWeight: 400,
        },
        icon: {
            color: themes[theme].menu.icon,
            [`&.${menuClasses.disabled}`]: {
                color: themes[theme].menu.disabled.color,
            },
        },
        SubMenuExpandIcon: {
            color: '#b6b7b9',
        },
        subMenuContent: ({ level }) => ({
            backgroundColor:
                level === 0
                    ? hexToRgba(themes[theme].menu.menuContent, hasImage && !collapsed ? 0.4 : 1)
                    : 'transparent',
        }),
        button: {
            [`&.${menuClasses.disabled}`]: {
                color: themes[theme].menu.disabled.color,
            },
            '&:hover': {
                backgroundColor: hexToRgba(themes[theme].menu.hover.backgroundColor, hasImage ? 0.8 : 1),
                color: themes[theme].menu.hover.color,
            },
        },
        label: ({ open }) => ({
            fontWeight: open ? 600 : undefined,
        }),
    };

    return (
        <div style={{ display: 'flex', height: '100vh', width: '100vw' }}>
            <Sidebar
                collapsed={collapsed}
                toggled={toggled}
                onBackdropClick={() => setToggled(false)}
                onBreakPoint={setBroken}
                rtl={rtl}
                breakPoint="md"
                backgroundColor={hexToRgba(themes[theme].sidebar.backgroundColor, hasImage ? 0.9 : 1)}
                rootStyles={{
                    color: themes[theme].sidebar.color,
                    height: '100vh',
                    position: 'fixed',
                }}
            >
                <div style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
                    <div style={{ padding: '16px 24px', display: 'flex', alignItems: 'center', justifyContent: rtl ? 'flex-end' : 'flex-start', marginBottom: '24px', marginTop: '16px' }}>
                        <h2 style={{ margin: 0, color: themes[theme].sidebar.color, margin: 'auto', display: 'flex', gap: 10, alignItems: 'center' }}>
                            <FaShoppingCart /> <span>Meats</span>
                        </h2>
                    </div>
                    <div style={{ flex: 1, marginBottom: '32px' }}>
                        <div style={{ padding: '0 24px', marginBottom: '8px' }}>General</div>
                        <Menu menuItemStyles={menuItemStyles}>
                            <SubMenu icon={<FaClipboardList />} label="Pedidos">
                                <MenuItem icon={<FaPlus />} onClick={() => navigate('pedidos/cadastrar')}>
                                    Cadastrar
                                </MenuItem>
                                <MenuItem icon={<FaList />} onClick={() => navigate('pedidos/listar')}>
                                    Listar
                                </MenuItem>
                            </SubMenu>

                            <SubMenu icon={<FaUser />} label="Compradores">
                                <MenuItem icon={<FaPlus />} onClick={() => navigate('compradores/cadastrar')}>
                                    Cadastrar
                                </MenuItem>
                                <MenuItem icon={<FaList />} onClick={() => navigate('compradores/listar')}>
                                    Listar
                                </MenuItem>
                            </SubMenu>

                            <SubMenu icon={<FaDrumstickBite />} label="Carnes">
                                <MenuItem icon={<FaPlus />} onClick={() => navigate('carnes/cadastrar')}>
                                    Cadastrar
                                </MenuItem>
                                <MenuItem icon={<FaList />} onClick={() => navigate('carnes/listar')}>
                                    Listar
                                </MenuItem>
                            </SubMenu>
                        </Menu>

                    </div>
                </div>
            </Sidebar>

            <main style={{ marginLeft: collapsed ? 80 : 250, flex: 1, padding: '16px 24px', }}>
                <div style={{ padding: '16px 24px', color: '#44596e' }}>
                    <Outlet />
                </div>
            </main>
        </div>
    );
};