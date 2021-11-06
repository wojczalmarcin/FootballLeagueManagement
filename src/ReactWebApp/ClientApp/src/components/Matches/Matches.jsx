import React, { useEffect, useState } from 'react';
import Dropdown from './Dropdown';
import { useHistory } from "react-router-dom";
import { HandleResponseError } from '../FetchData/ResponseErrorHandling';

const Matches = () => {
    const [loading, setLoading] = useState(true);

    const [matches, setMatches] = useState([]);

    const [seasons, setSeasons] = useState([]);

    const [editing, setEditing] = useState(0);

    const [adding, setAdding] = useState(false);

    const [inputs, setInputs] = useState({});

    const [currentSeason, setCurrentSeason] = useState({});

    const history = useHistory();

    const loadSeasons = () => {
        setLoading(true);

        fetch('Api/Season')
            .then(response => response.json())
            .then((data) => {
                if (data.responseStatus !== 200) {
                    HandleResponseError(data);
                }
                else {
                    setSeasons(data.data);
                    setCurrentSeason(data.data[0]);
                }
            })
            .catch((error) => {
                alert(error);
            })
            .finally(() => {
                setLoading(false);
            });
    };

    const loadMatches = (currentSeasonId) => {
        fetch(`Api/Match?seasonId=${currentSeasonId}`)
            .then(response => response.json())
            .then((matchesData) => {
                if (matchesData.responseStatus !== 200) {
                    HandleResponseError(matchesData);
                }
                else {
                    setMatches(matchesData.data);
                }
            })
            .catch((error) => {
                alert(error);
            })
    }

    const handleStats = (matchId) => {
        history.push(`/match-stats/${matchId}`);
    };

    const handleEdit = (season) => {
        setInputs(season);
        setEditing(true);
    };

    const handleCancelEdit = (season) => {
        setInputs(season);
        setEditing(false);
    };

    const handleSave = () => {
        fetch('api/Season', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(response => response.json())
            .then(data => {
                console.log('Response:', data);
                if (data.responseStatus !== 200) {
                    alert(data.validationErrors);
                }
            })
            .catch(error => {
                console.error('Error:', error);
            })
            .finally(() => {
                setEditing(false);
            });
    }

    const handleAdd = () => {
        setInputs({ startDate: "", endDate: "", sponsor: "" });
        setAdding(true);
    }

    const handlePost = () => {
        fetch('api/Season', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(response => response.json())
            .then(data => {
                console.log('Response:', data);
                if (data.responseStatus !== 200) {
                    alert(data.validationErrors);
                }
            })
            .catch(error => {
                console.error('Error:', error);
            })
            .finally(() => setAdding(false));
    }

    const handleCancelAdd = () => {
        setAdding(false);
    }

    const handleInputChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    };

    useEffect(() => {
        loadSeasons();
    }, []);

    useEffect(() => {
        if(currentSeason.id)
        {
            loadMatches(currentSeason.id);
        }
    }, [currentSeason]);

    useEffect(() => {
        if (editing === false) {
            loadSeasons();
        }
    }, [editing]);

    useEffect(() => {
        if (adding === false) {
            loadSeasons();
        }
    }, [adding]);

    const renderMatches = () => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Gospodarz</th>
                        <th>Gość</th>
                        <th>Wynik</th>
                        <th>Data</th>
                        <th>Adres</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {matches.map(match => {
                        return (
                            <tr key={match.id}>
                                <td>{match.teamHome.name}</td>
                                <td>{match.teamAway.name}</td>
                                <td>{match.matchScore ? match.matchScore.homeGoals + ":" + match.matchScore.awayGoals : ""}</td>
                                <td>{match.date}</td>
                                <td>{match.address ? match.address.city + " " + match.address.street + " " + match.address.houseNumber : ""}</td>
                                <td>
                                    <button className='btn-primary' disabled={match.matchScore}>Usuń</button>
                                    <button className='btn-primary'onClick={() => handleStats(match.id)}>Statystyki</button>
                                </td>
                            </tr>)
                    }
                    )}
                </tbody>
            </table>

        );
    }

    return (
        loading
            ? <p><em>Ładuję...</em></p>
            :
            <div>
                <Dropdown seasons={seasons} currentSeason={currentSeason} setCurrentSeason={setCurrentSeason} />
                {renderMatches()}
            </div>
    );
}

export default Matches;