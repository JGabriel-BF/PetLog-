import style from "./TopbarItem.module.css"
import { Link } from "react-router-dom"

export function TopbarItem ({texto, link, logo}){
    return (
        <Link to={link} className={style.topbar_item}>
            {logo}
            <h2 className={style.texto_link}> {texto}    </h2>
        </Link>
    )
}