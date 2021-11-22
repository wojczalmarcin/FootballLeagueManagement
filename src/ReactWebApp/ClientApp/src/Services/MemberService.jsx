import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadPlayersByMatchId = (setPlayers, matchId) => {
    return (
        fetch(`Api/Member/Match/${matchId}`)
            .then(HandleResponseErrorGet)
            .then((data) => setPlayers(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const LoadMembersByTeamId = (setMembers, teamId, pageSize, pageNumber) => {
    return (
        fetch(`Api/Member/Team/${teamId}?pageSize=${pageSize}&pageNumber=${pageNumber}`)
            .then(HandleResponseErrorGet)
            .then((data) => setMembers(data))
            .catch((error) => {
                console.log(error);
                alert(error);
            })
    );
}

export const LoadNumberOfMembersByTeamId = (setNumberOfMembers, teamId) =>
{
    return (
        fetch(`Api/Member/Count?teamId=${teamId}`)
            .then(HandleResponseErrorGet)
            .then((data) => setNumberOfMembers(data))
            .catch((error) => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const EditMember = (inputs) => {
    return (
        fetch('Api/Member', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}
 