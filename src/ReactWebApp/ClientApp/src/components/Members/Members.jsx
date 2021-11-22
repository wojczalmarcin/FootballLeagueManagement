import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from "react-router-dom";
import { LoadMembersByTeamId, LoadNumberOfMembersByTeamId } from '../../Services/MemberService';
import ReactPaginate from 'react-paginate';
import MemberRow from './MemberRow';

const Members = () => {

    const { teamId, pageNumber } = useParams();

    const [loading, setLoading] = useState(true);

    const [members, setMembers] = useState([]);

    const [numberOfMembers, setNumberOfMembers] = useState(0);

    const [editing, setEditing] = useState(0);

    const history = useHistory();

    const pageSize = 10;

    const handlePageChange = (event) => {
        var page = parseInt(event.selected) + 1;
        if(page !== parseInt(pageNumber))
        {
            history.replace(`/members/${teamId}/${page}`);
            history.go(0)
        }
    }
      
    //const handleStats = (matchId) => {
    //    history.push(`/match-stats/${matchId}`);
    //};

    const loadMembers = () => {
        setLoading(true);
        LoadMembersByTeamId(setMembers, teamId, pageSize, pageNumber)
            .then(() => LoadNumberOfMembersByTeamId(setNumberOfMembers, teamId))
            .then(() => setLoading(false));
    };

    useEffect(() => {
        loadMembers();
    }, []);

    useEffect(() => {
        if(editing === 0)
        {
            loadMembers();
        }
    }, [editing]);

    const renderMembers = () => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Imię</th>
                        <th>Nazwisko</th>
                        <th>Drużyna</th>
                        <th>Rola</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {members.map(member => {
                        return (
                            <MemberRow 
                                member={member}
                                editing={editing}
                                setEditing={setEditing}
                                key={member.id}
                            />
                        )
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
                {/*<Dropdown seasons={seasons} currentSeason={currentSeason} setCurrentSeason={setCurrentSeason} />*/}
                {renderMembers()}
                <ReactPaginate
                    className="paginate"
                    initialPage={pageNumber-1}
                    breakLabel="..."
                    nextLabel=" >"
                    onPageChange={handlePageChange}
                    pageRangeDisplayed={5}
                    pageCount={Math.ceil(numberOfMembers/pageSize)}
                    previousLabel="< "
                    renderOnZeroPageCount={null}
                    activeClassName="paginate-current-page"
                    pageClassName="paginate-page"
                />
            </div>
    );
}

export default Members;