import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import './custom.css'
import Seasons from './components/Seasons/Seasons';
import Matches from './components/Matches/Matches';
import MatchStats from './components/Matches/MatchStats';
import Members from './components/Members/Members';
import Teams from './components/Teams/Teams';
import { BrowserRouter, Switch } from "react-router-dom";
import LeagueTable from './components/Matches/LeagueTable';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <BrowserRouter forceRefresh={true} />
        <Switch>
          <Route exact path='/' component={Home} />
          <Route path='/league-table' component={LeagueTable} />
          <Route path='/seasons' component={Seasons} />
          <Route path='/matches' component={Matches} />
          <Route path='/match-stats/:matchId' component={MatchStats} />
          <Route path='/members/:teamId/:pageNumber' component={Members} />
          <Route path='/teams' component={Teams} />
        </Switch>
        <BrowserRouter />
      </Layout>
    );
  }
}
