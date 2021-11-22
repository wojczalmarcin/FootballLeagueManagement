import React, { useState , useEffect} from 'react';
import { EditMember } from '../../Services/MemberService';
import { LoadAllTeams } from '../../Services/TeamService';
import TeamPicker from '../Matches/Pickers/TeamPicker';

const MemberRow = (props) => {

    const [loading, setLoading] = useState(false);

    const [inputs, setInputs] = useState(props.member);

    const [teams, setTeams] = useState([]);

    const [team, setTeam] = useState(props.member.team);

    const handleEdit = () => {
        setLoading(true);
        setInputs(props.member);
        setTeam(props.member.team);
        LoadAllTeams(setTeams)
            .finally(()=>{setLoading(false); props.setEditing(props.member.id)});
    };

    const handleCancelEdit = () => {
        setInputs(props.member);
        props.setEditing(0);
    };

    const handleInputChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    };

    const handleSave = () => {
        inputs.teamId = team.id;

        var editInputs = {};
        editInputs.firstName = inputs.firstName;
        editInputs.lastName = inputs.lastName;
        editInputs.teamId = team.id
        console.log(inputs);
        setLoading(true);
        EditMember(inputs)
            .finally(() => {
                props.setEditing(0);
                setLoading(false);
            });
    }

    if (loading) {
        return (
            <tr>
                <td>{props.member.firstName}</td>
                <td>{props.member.lastName}</td>
                <td>{props.member.team ? props.member.team.name : ""}</td>
                <td>{props.member.memberRole ? props.member.memberRole.name : ""}</td>
                <td>
                    Loading...
                </td>
            </tr>);
    }
    if (props.editing !== props.member.id) {
        return (
            <tr>
                <td>{props.member.firstName}</td>
                <td>{props.member.lastName}</td>
                <td>{props.member.team ? props.member.team.name : ""}</td>
                <td>{props.member.memberRole ? props.member.memberRole.name : ""}</td>
                <td>
                    <button className='btn-primary' onClick={handleEdit}>Edytuj</button>
                    <button className='btn-invisible' disabled={true}></button>
                </td>
            </tr>);
    }

    return (
        <tr>
            <td>
                <input
                    name="firstName"
                    type="text"
                    value={inputs.firstName ? inputs.firstName : ""}
                    onChange={handleInputChange}
                />
            </td>
            <td>
                <input
                    name="lastName"
                    type="text"
                    value={inputs.lastName ? inputs.lastName : ""}
                    onChange={handleInputChange}
                />
            </td>
            <td>
                <TeamPicker 
                    teams={teams} 
                    setCurrentTeam={setTeam} 
                    team={team} 
                />
            </td>
            <td>{props.member.memberRole ? props.member.memberRole.name : ""}</td>
            <td>
                <button className='btn-primary' onClick={handleCancelEdit}>Anuluj</button>
                <button className='btn-primary' onClick={handleSave}>Zapisz</button>
            </td>
        </tr>);

}

export default MemberRow;