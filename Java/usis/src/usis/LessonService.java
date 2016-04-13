package usis;

import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.UriInfo;

import org.apache.log4j.Logger;

import to.ImageTO;
import to.LessonScreenTO;
import to.QuestionTO;
import to.ScreenTO;

@Path("/lessons")
public class LessonService {
	@Context
	UriInfo uriInfo;
	static final Logger LOGGER = Logger.getLogger(LessonService.class);

	/*@GET
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<Lesson> getAllLessons(@Context HttpServletRequest request) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getLessons();
	}
	*/
	/*
	@GET @Path("{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public Lessons getLessonById(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getLessonById(id);
	}
	*/
	@GET 
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<String> getLessonURLList(@Context HttpServletRequest request) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getLessonURLList();
	}
	
	@GET @Path("{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public LessonScreenTO getLessonWithId(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getLessonWihScreenById(id);
	}
	
	@GET @Path("screens/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<Screen> getScreensByLessonId(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getScreensLessonById(id);
	}
	@GET @Path("screensbylesson/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<ScreenBrief> getScreensByLesson(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getScreenBriefLessonById(id);
	}
	
	@GET @Path("one-screen/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public Screens getOneScreensById(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getOneScreenById(id);
	}
	@GET @Path("{lessonid}/screens/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public ScreenTO getScreenWithId(@Context HttpServletRequest request,@PathParam("id") String id,@PathParam("lessonid") String lessonid) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		
		return LessonDAO.getScreenWithId(id,lessonid);
	}
	
	@GET @Path("{lessonid}/screens/{screenid}/images/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public ImageTO getImageWithId(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return ImageDAO.getImageWithId(id);
	}
	
	@GET @Path("{lessonid}/screens/{screenid}/options/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public QuestionTO getQuestionWithId(@Context HttpServletRequest request,@PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		
		LOGGER.info("IP:" + ip);
		return QuestionDAO.getQuestionWithId(id);
	}
}
