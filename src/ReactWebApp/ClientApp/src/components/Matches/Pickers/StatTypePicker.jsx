import React, { useState } from 'react';
import classes from "../Dropdown.module.css";

const StatTypePicker = (props) => {

    const [toggle, setToggle] = useState(false);

    const handleClick = (statType) => {
        props.setStatType(statType);
        setToggle(false);
    }

    return (
        <div className={classes.dropdown}>
            <input
                readOnly
                name="statType"
                type="text"
                value={props.statType.statName}
                onClick={() => setToggle(!toggle)}
            />
            {toggle && props.statTypes.length > 0 ?
                <div className={classes.dropdownContent}>
                    {props.statTypes.map(statType => {
                        return (
                            <p key={statType.id} onClick={() => handleClick(statType)}>
                                {statType.statName}
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

export default StatTypePicker;