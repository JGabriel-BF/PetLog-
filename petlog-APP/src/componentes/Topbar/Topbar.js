import style from "./Topbar.module.css";
import { Link } from "react-router-dom";
import { MdLogout } from "react-icons/md"
import Logo from "../../assets/LogoPetLog.png"
import { TopbarItem } from "../TopbarItem/TopbarItem";

export function Topbar ({children, childrenTopo}){
    function deslogar()
    {
        localStorage.removeItem("vendedor")
    }

    return (
        <div>
            <div className={style.topbar_conteudo}>
                
                    <img src={Logo} alt="Logo PetLog" className={style.logoPet} />
                

                <div className={style.area_centro_topo}>
                        <TopbarItem texto = "Produtos" link="/produtos"/>
                        <TopbarItem texto = "Clientes" link="/clientes"/>
                        <TopbarItem texto= "Home" link="/home" />
                        <TopbarItem texto= "Compras" link="/compras" />
                    {childrenTopo}
                </div>

                <Link to= "/" className={style.botao_deslogar} onClick={deslogar}>
                    <MdLogout />
                </Link>
            </div>

            <div className={style.pagina_conteudo}>
                
                {children}
            </div>
        </div>
    );
}