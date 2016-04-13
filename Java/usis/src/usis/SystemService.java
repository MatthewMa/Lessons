package usis;

import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.UriInfo;

import org.apache.log4j.Logger;

import to.UserTO;

@Path("/system")
public class SystemService {
	
	@Context
	UriInfo uriInfo;
	static final Logger LOGGER = Logger.getLogger(LessonService.class);
	
	@GET
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<String> getSystemUser() {
		return SystemDAO.getUsersURLList();
	}
	
	@GET @Path("{id}/user")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public UserTO getUserById(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return SystemDAO.getUser(id);
	}
	
	@POST @Path("login")
	@Consumes("application/json")
	@Produces("application/json")
	public boolean login(UserTO userTO){
		boolean logged=false;
		try {
			logged=SystemDAO.login(userTO);
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return logged;
	}
	
}
