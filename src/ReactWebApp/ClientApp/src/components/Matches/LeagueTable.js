import React, { Component } from 'react';

export class LeagueTable extends Component {
    static displayName = LeagueTable.name;

    constructor(props) {
        super(props);
        this.state = { teams: [], loading: true, error: false };
    }

    componentDidMount() {
        this.populateLeagueTableData();
    }

    static renderLeagueTable(teams) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Team</th>
                        <th>Matches</th>
                        <th>Points</th>
                        <th>Goals (H)</th>
                        <th>Goals (A)</th>
                    </tr>
                </thead>
                <tbody>
                    {teams.map(team =>
                        <tr key={team.name}>
                            <td>{team.name}</td>
                            <td>{team.matchesPlayed}</td>
                            <td>{team.points}</td>
                            <td>{team.goalsHome}</td>
                            <td>{team.goalsAway}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : LeagueTable.renderLeagueTable(this.state.teams);

        return (
            <div>
                <h1 id="tabelLabel" >Season table</h1>
                <p></p>
                {contents}
            </div>
        );
    }

    populateLeagueTableData() {
        fetch('Api/Match/Table/1')
            .then(response => response.json())
            .then((data) => {
                console.log(data);
                this.setState({ teams: data });
                this.setState({ loading: false });
            })
            .catch((error) => {
                console.log("ERROR");
                console.log(error);
            })
            .finally(() => {
                //this.setState({ loading: false });
            });     
    }
}
