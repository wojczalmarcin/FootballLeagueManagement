import React, { useState } from 'react';
import classes from "./Dropdown.module.css";

const TeamPicker = (props) => {

    const [toggle, setToggle] = useState(false);

    const handleClick = (team) => {
        props.setCurrentTeam(team);
        setToggle(false);
    }

    return (
        <div className={classes.dropdown}>
            <input
                readOnly
                name="teamHome"
                type="text"
                value={props.team.name}
                onClick={() => setToggle(!toggle)}
            />
            {toggle && props.teams.length > 0 ?
                <div className={classes.dropdownContent}>
                    {props.teams.map(team => {
                        return (
                            <p key={team.id} onClick={() => handleClick(team)}>
                                {team.name}
                            </p>
                        )
                    })
                    }
                </div>
                :
                <div></div>
            }
        </div>
    );
}

export default TeamPicker;