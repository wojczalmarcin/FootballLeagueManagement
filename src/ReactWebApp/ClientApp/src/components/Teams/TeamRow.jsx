import React, { useState } from 'react';
import { useHistory } from "react-router-dom";
import { EditTeam } from '../../Services/TeamService';

const TeamRow = (props) => {

    const [loading, setLoading] = useState(false);

    const [inputs, setInputs] = useState(props.team);

    const history = useHistory();

    const handleMembers = () => {
        history.push(`/members/${props.team.id}/1`);
    };

    const handleEdit = () => {
        setInputs(props.team);
        props.setEditing(props.team.id);
    };

    const handleCancelEdit = () => {
        setInputs(props.team);
        props.setEditing(0);
    };

    const handleInputChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    };

    const handleSave = () => {
        setLoading(true);
        EditTeam(inputs)
            .finally(() => {
                props.setEditing(0);
                setLoading(false);
            });
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
    if (props.editing !== props.team.id) {
        return (
            <tr>
                <td>{props.team.name}</td>
                <td>
                    <button className='btn-primary-bigger' onClick={handleMembers}>Członkowie</button>
                    <button className='btn-primary' onClick={handleEdit}>Edytuj</button>
                </td>
            </tr>);
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
                <button className='btn-primary-bigger' onClick={handleSave}>Zapisz</button>
                <button className='btn-primary' onClick={handleCancelEdit}>Anuluj</button>
            </td>
        </tr>);

}

export default TeamRow;