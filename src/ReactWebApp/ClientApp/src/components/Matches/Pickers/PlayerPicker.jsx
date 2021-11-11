import React, { useState, useEffect } from 'react';
import classes from "../Dropdown.module.css";

const PlayerPicker = (props) => {

    const [toggle, setToggle] = useState(false);

    const [textField, setTextField] = useState("");

    const handleClick = (player) => {
        props.setPlayer(player);
        setToggle(false);
    }

    useEffect(() => {
        if (props.team) {
            setTextField("");
            props.setPlayer({});
        }
    }, [props.team]);

    useEffect(() => {
        var playerFullName = "";
        if (props.player.firstName) {
            playerFullName = props.player.firstName + " " + props.player.lastName;
        }
        setTextField(playerFullName);
    }, [props.player]);

    return (
        <div className={classes.dropdown}>
            <input
                readOnly
                name="player"
                type="text"
                value={textField}
                onClick={() => setToggle(!toggle)}
            />
            {toggle && props.players.length > 0 ?
                <div className={classes.dropdownContent}>
                    {props.players.map(player => {
                        if ((player.team.id === props.team.id && props.team.id) || !props.team.id) {
                            return (
                                <p key={player.id} onClick={() => handleClick(player)}>
                                    {player.firstName + " " + player.lastName}
                                </p>
                            )
                        }
                        else {
                            return <span></span>
                        }
                    })
                    }
                </div>
                :
                <div></div>
            }
        </div>
    );
}

export default PlayerPicker;