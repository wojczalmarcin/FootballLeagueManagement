import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from "react-router-dom";
import { LoadSeasonsCurrentSeason } from '../../Services/SeasonService';
import { LoadAllTeams, LoadTeams } from '../../Services/TeamService';
import Dropdown from '../SeasonDropdown/Dropdown';
import { LoadNumberOfMembersByTeamId } from '../../Services/MemberService';

import ReactPaginate from 'react-paginate';
import TeamRow from './TeamRow';
import TeamAddRow from './TeamAddRow';

const Teams = () => {

    const [loading, setLoading] = useState(true);

    const [teams, setTeams] = useState([]);

    const [currentTeam, setCurrentTeam] = useState({});

    const [editing, setEditing] = useState(0);

    const [adding, setAdding] = useState(false);

    const [seasons, setSeasons] = useState([]);

    const [currentSeason, setCurrentSeason] = useState({});

    const [showAll, setShowAll] = useState(false);

    const loadSeasons = () => {
        setLoading(true);
        LoadSeasonsCurrentSeason(setSeasons, setCurrentSeason)
            .then(() => setLoading(false));
    };

    useEffect(() => {
        if(!showAll){
            if (currentSeason.id) {
                LoadTeams(setTeams, currentSeason.id);
            }
        }
        else{
            LoadAllTeams(setTeams);
        }
    }, [currentSeason]);

    useEffect(() => {
        if (teams[0]) {
            if (teams[0].id) {
                setCurrentTeam(teams[0])
            }
        }
    }, [teams]);

    useEffect(() => {
        loadSeasons();
    }, [editing]);

    useEffect(() => {
        loadSeasons();
    }, [adding]);

    useEffect(() => {
        if(showAll === true)
        {
            loadSeasons();
        }
    }, [showAll]);

    const renderTeams = () => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Drużyna</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {teams.map(team => {
                        return (
                            <TeamRow
                                team={team}
                                key={team.id}
                                editing={editing}
                                setEditing={setEditing}
                            />
                        )
                    }
                    )}
                    { showAll &&
                    <TeamAddRow 
                        editing={editing} 
                        adding={adding} 
                        setAdding={setAdding}
                    />}
                </tbody>
            </table>

        );
    }

    return (
        loading
            ? <p><em>Ładuję...</em></p>
            :
            <div>
                <Dropdown 
                    seasons={seasons}
                    currentSeason={currentSeason} 
                    setCurrentSeason={setCurrentSeason}
                    showAll={showAll}
                    setShowAll={setShowAll} />
                {renderTeams()}
            </div>
    );
}

export default Teams;