import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadTeams = (setTeams, seasonId) => {
    return(
    fetch(`Api/Team/Season/${seasonId}`)
        .then(response => response.json())
        .then((data) => {
            if (data.responseStatus !== 200) {
                HandleResponseErrorGet(data);
            }
            else {
                setTeams(data.data);
            }
        })
        .catch((error) => {
            console.error('Error:', error);
        })
    )
}