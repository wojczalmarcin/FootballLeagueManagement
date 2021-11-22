import React, { useState } from 'react';
import { AddTeam } from '../../Services/TeamService';

const TeamAddRow = (props) => {

    const [loading, setLoading] = useState(false);

    const [inputs, setInputs] = useState({});

    const handleAdd = () => {
        props.setAdding(true);
    }

    const handlePost = () => {
        setLoading(true);
        AddTeam(inputs)
            .finally(()=>{
                setLoading(false);
                props.setAdding(false);
            })
    }

    const cancelAdding = () => {
        props.setAdding(false);
    }

    if (loading) {
        return (
            <tr>
                <td>{props.team.name}</td>
                <td>
                    Loading...
                </td>
            </tr>);
    }

    const handleInputChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    };

    if (!props.adding) {
        return (
            <tr>
                <td></td>
                <td>
                    <button className='btn-primary' disabled={props.editing} onClick={handleAdd}>Dodaj</button>
                    <button className='btn-invisible' disabled={true}></button>
                </td>
            </tr>
        );
    }

    return (
        <tr>
            <td>
                <input
                    name="name"
                    type="text"
                    value={inputs.name ? inputs.name : ""}
                    onChange={handleInputChange}
                />
            </td>
            <td>
                <button className='btn-primary' disabled={props.editing} onClick={cancelAdding}>Anuluj</button>
                <button className='btn-primary' disabled={props.editing} onClick={handlePost}>Dodaj</button>
            </td>
        </tr>
    );
}

export default TeamAddRow;