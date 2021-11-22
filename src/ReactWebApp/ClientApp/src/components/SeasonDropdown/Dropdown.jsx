import React, { useState } from 'react';
import classes from "./Dropdown.module.css";

const Dropdown = (props) => {

    const [toggle, setToggle] = useState(false);
    
    const handleClick = (season) => {
        props.setCurrentSeason(season);
        if(props.setShowAll)
        {
            props.setShowAll(false);
        }
        setToggle(false);
    }

    const handleClickAll = () => {
        props.setShowAll(true);
        props.setCurrentSeason({});
        setToggle(false);
    }

    return (
        <div className={classes.dropdown}>
            <button onClick={() => setToggle(!toggle)} className={classes.dropbtn}>
                {!props.showAll ?
                new Date(props.currentSeason.startDate).getFullYear() + "/" + (props.currentSeason.endDate ? new Date(props.currentSeason.endDate).getFullYear() : "")
                :
                "Wszystkie"
                }
            </button>
            {toggle? 
                <div className={classes.dropdownContent}>
                    {props.seasons.map(season => {
                        return (
                            <p key={season.id} onClick={()=>handleClick(season)}>
                                {new Date(season.startDate).getFullYear() + "/" + (season.endDate ? new Date(season.endDate).getFullYear() : "")}
                            </p>
                            )
                        })
                    }
                    {props.showAll!== undefined ?
                        <p onClick={handleClickAll}>
                            Wszystkie
                        </p>
                        :
                        <span></span>
                    }
                </div>
                :
                <div></div>
            }
        </div>
    );
}

export default Dropdown;