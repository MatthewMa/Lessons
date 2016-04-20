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
import to.LessonBr;
import to.LessonScreenTO;
import to.QuestionTO;
import to.ScreenComplete;
import to.ScreenTO;

@Path("/lessons")
public class LessonService {
	@Context
	UriInfo uriInfo;
	static final Logger LOGGER = Logger.getLogger(LessonService.class);

	@GET
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<LessonBr> getLessonURLList(@Context HttpServletRequest request) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		
		return LessonDAO.getBriefLessons();
	}

	@GET
	@Path("{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public LessonBr getLessonWithId(@Context HttpServletRequest request, @PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);

		return LessonDAO.getLessonBriefLessonById(id);
	}
	
	
	@GET
	@Path("{lessonid}/screens")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<Screen> getListOfScreens(@Context HttpServletRequest request, @PathParam("lessonid") String lessonid) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getScreensLessonById(lessonid);
	}
	
	
	@GET
	@Path("{lessonid}/screens/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public ScreenComplete getScreenWithId(@Context HttpServletRequest request, @PathParam("id") String id,
			@PathParam("lessonid") String lessonid) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getCompleteScreensById(id);
	}
	
	@GET
	@Path("{lessonid}/screens/position/{position}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public ScreenComplete getScreenByPosition(@Context HttpServletRequest request, @PathParam("id") String id,
			@PathParam("lessonid") String lessonid,@PathParam("position") Integer position) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getScreenByPosition(lessonid, position);
	}
	
	@GET
	@Path("{lessonid}/screens/nextscreen/{screenid}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public ScreenComplete getScreenByNextPosition(@Context HttpServletRequest request,
			@PathParam("lessonid") String lessonid,@PathParam("screenid") Integer currentScreenId) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getNextScreenByCurrentScreen(currentScreenId);
	}
	
	
	@GET
	@Path("{lessonid}/screens/{screenid}/images")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<Images> getListOfImagesByScreenId(@Context HttpServletRequest request,
			@PathParam("screenid") String screenId) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return ImageDAO.getListofImages(screenId);
	}
	
	@GET
	@Path("{lessonid}/screens/{screenid}/images/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public ImageTO getImageWithId(@Context HttpServletRequest request, @PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return ImageDAO.getImageWithId(id);
	}
	
	
	@GET
	@Path("{lessonid}/screens/{screenid}/options")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<Questions> getListOfOptionsByScreenId(@Context HttpServletRequest request,
			@PathParam("screenid") String screenId) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);

		LOGGER.info("IP:" + ip);
		return QuestionDAO.getListOfOptionsByScreenId(screenId);
	}
	

	@GET
	@Path("{lessonid}/screens/{screenid}/options/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public QuestionTO getQuestionWithId(@Context HttpServletRequest request, @PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);

		LOGGER.info("IP:" + ip);
		return QuestionDAO.getQuestionWithId(id);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	@GET
	@Path("screens/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<Screen> getScreensByLessonId(@Context HttpServletRequest request, @PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getScreensLessonById(id);
	}

	@GET
	@Path("screensbylesson/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<ScreenBrief> getScreensByLesson(@Context HttpServletRequest request, @PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getScreenBriefLessonById(id);
	}

	@GET
	@Path("one-screen/{id}")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public Screens getOneScreensById(@Context HttpServletRequest request, @PathParam("id") String id) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getOneScreenById(id);
	}

	

	@GET
	@Path("/list")
	@Produces({ MediaType.APPLICATION_JSON, MediaType.APPLICATION_JSON })
	public List<Lesson> getListOfLessons(@Context HttpServletRequest request, @PathParam("id") String id,
			@PathParam("lessonid") String lessonid) {
		String ip = request.getRemoteAddr();
		System.out.println("IP:" + ip);
		LOGGER.info("IP:" + ip);
		return LessonDAO.getLessons();
	}
	
	
	
	
	
	

	
}
